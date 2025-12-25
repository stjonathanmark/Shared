namespace Shared.Data;

public interface IBaseUnitOfWork
{
    void Commit();

    Task CommitAsync();

    void CommitTransaction();

    Task CommitTransactionAsync();

    void RollbackTransaction();

    Task RollbackTransactionAsync();
    void StartTransaction();

    Task StartTransactionAsync();
}
