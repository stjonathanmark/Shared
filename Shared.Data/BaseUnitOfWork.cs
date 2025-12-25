using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Data.Repositories;

namespace Shared.Data;

public class BaseUnitOfWork : IBaseUnitOfWork
{
    protected readonly DbContext context;
    protected readonly Dictionary<string, object> repos = [];
    private IDbContextTransaction? transaction = null;

    public BaseUnitOfWork(DbContext context)
    { 
        this.context = context;
    }

    public bool TransactionInProcess => transaction != null;

    public void Commit()
    {
        if (context.ChangeTracker.HasChanges())
            context.SaveChanges();
    }

    public async Task CommitAsync()
    {
        if (context.ChangeTracker.HasChanges())
            await context.SaveChangesAsync();
    }

    public void CommitTransaction()
    {
        transaction?.Commit();
    }

    public async Task CommitTransactionAsync()
    {
        await transaction!.CommitAsync();
    }

    public void RollbackTransaction()
    {
        transaction?.Rollback();
    }

    public async Task RollbackTransactionAsync()
    {
        await transaction!.RollbackAsync();
    }

    public void StartTransaction()
    {
        transaction = context.Database.BeginTransaction();
    }

    public async Task StartTransactionAsync()
    {
        transaction = await context.Database.BeginTransactionAsync();
    }

    protected TRepository GetRepository<TRepository, TEntity>()
        where TEntity : class
        where TRepository : IBaseRepository<TEntity>
    {
        var type = typeof(TEntity);
        var name = type.Name;

        if (!repos.ContainsKey(name))
        {
            repos.Add(name, Activator.CreateInstance(typeof(TRepository), [context])!);
        }

        return (TRepository)repos[name];
    }
}
