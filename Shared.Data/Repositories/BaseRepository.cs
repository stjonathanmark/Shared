using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Shared.Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    protected readonly DbSet<TEntity> set;

    public BaseRepository(DbContext context)
    {
        set = context.Set<TEntity>();
    }

    public int EntitiesFound { get; set; }

    #region Create

    public void Create(TEntity entity)
    {
        AddCreationDate(entity);
        set.Add(entity);
    }

    public void Create(IEnumerable<TEntity> entities)
    {
        AddCreationDate(entities);
        set.AddRange(entities);
    }

    public async Task CreateAsync(TEntity entity)
    {
        AddCreationDate(entity);
        await set.AddAsync(entity);
    }

    public async Task CreateAsync(IEnumerable<TEntity> entities)
    {
        AddCreationDate(entities);
        await set.AddRangeAsync(entities);
    }

    #endregion

    #region Read

    public List<TEntity> Get(string? orderBy = null, int? pageNumer = null, int? pageSize = null, IEnumerable<string>? includes = null, bool includeDeleted = false)
    {
        return GetEntities(null, null, pageNumer, pageSize, orderBy, includes, includeDeleted);
    }

    public TEntity? Get(params object[] id)
    {
        return set.Find(id);
    }

    public List<TEntity> Get(Expression<Func<TEntity, bool>> predicate, string? orderBy = null, int? pageNumer = null, int? pageSize = null, IEnumerable<string>? includes = null, bool includeDeleted = false)
    {
        return GetEntities(predicate, null, pageNumer, pageSize, orderBy, includes, includeDeleted);
    }

    public List<TEntity> Get(string filter, string? orderBy = null, int? pageNumer = null, int? pageSize = null, IEnumerable<string>? includes = null, bool includeDeleted = false)
    {
        return GetEntities(null, filter, pageNumer, pageSize, orderBy, includes, includeDeleted);
    }

    public async Task<TEntity?> GetAsync(params object[] id)
    {
        return await set.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAsync(string? orderBy = null, int? pageNumer = null, int? pageSize = null, IEnumerable<string>? includes = null, bool includeDeleted = false)
    {
        return await GetEntitiesAsync(null, null, pageNumer, pageSize, orderBy, includes, includeDeleted);
    }

    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, string? orderBy = null, int? pageNumer = null, int? pageSize = null, IEnumerable<string>? includes = null, bool includeDeleted = false)
    {
        return await GetEntitiesAsync(predicate, null, pageNumer, pageSize, orderBy, includes, includeDeleted);
    }

    public async Task<List<TEntity>> GetAsync(string filter, string? orderBy = null, int? pageNumer = null, int? pageSize = null, IEnumerable<string>? includes = null, bool includeDeleted = false)
    {
        return await GetEntitiesAsync(null, filter, pageNumer, pageSize, orderBy, includes, includeDeleted);
    }

    #endregion

    #region Update

    public void Update(TEntity entity)
    {
        AddModificationDate(entity);
        set.Update(entity);
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        AddModificationDate(entities);
        set.UpdateRange(entities);
    }

    #endregion

    #region Delete

    public void Delete(TEntity entity, bool permanent = false)
    {
        if (entity == null)
            return;

        if (!SoftDeleted(permanent, entity))
            set.Remove(entity);
    }

    public void Delete(IEnumerable<TEntity> entities, bool permanent = false)
    {
        if (!entities.Any())
            return;

        if (!permanent && entities.First() is ISoftDeleted)
            SoftDelete(entities);
        else
            set.RemoveRange(entities);
    }

    #endregion

    #region Creation Helper Methods

    protected void AddCreationDate(TEntity entity)
    {
        var now = DateTime.Now;
        if (entity is IHasCreationDate created)
        {
            created.CreationDate = now;
        }
    }

    protected void AddCreationDate(IEnumerable<TEntity> entities)
    {
        var creationDate = DateTime.Now;

        if (entities.Any() && entities.First() is IHasCreationDate)
        {
            foreach (var entity in entities)
            {
                var created = (IHasCreationDate)entity;
                created.CreationDate = creationDate;
            }
        }
    }

    #endregion

    #region Read Helper Methods

    protected List<TEntity> GetEntities(Expression<Func<TEntity, bool>>? predicate = null, string? filter = null, int? pageNumber = null, int? pageSize = null, string? orderBy = null, IEnumerable<string>? includes = null, bool includeDeleted = false)
    {
        var query = GetCountQuery(predicate, filter, includeDeleted);

        EntitiesFound = query.Count();

        query = GetDataQuery(query, pageNumber, pageSize, orderBy, includes);

        return [.. query];
    }


    protected async Task<List<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>>? predicate = null, string? filter = null, int? pageNumber = null, int? pageSize = null, string? orderBy = null, IEnumerable<string>? includes = null, bool includeDeleted = false)
    {
        var query = GetCountQuery(predicate, filter, includeDeleted);

        EntitiesFound = query.Count();

        query = GetDataQuery(query, pageNumber, pageSize, orderBy, includes);

        return await query.ToListAsync();
    }

    private IQueryable<TEntity> GetCountQuery(Expression<Func<TEntity, bool>>? predicate = null, string? filter = null, bool includeDeleted = false)
    {
        var query = predicate is null
            ? filter is null
                ? set
                : set.Where(filter)
            : set.Where(predicate);

        if (typeof(TEntity) is not ISoftDeleted || includeDeleted)
            query = query.Where(e => !((ISoftDeleted)e).Deleted);

        return query;
    }

    private IQueryable<TEntity> GetDataQuery(IQueryable<TEntity> query, int? pageNumber = null, int? pageSize = null, string? orderBy = null, IEnumerable<string>? includes = null)
    {
        var take = pageSize;
        var skip = (pageNumber.HasValue && pageSize.HasValue) ? (pageNumber.Value - 1) * pageSize : null;

        EntitiesFound = query.Count();

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        if (!string.IsNullOrEmpty(orderBy))
            query = query.OrderBy(orderBy);

        if (includes!.Any())
            foreach (var include in includes!)
                query = query.Include(include);

        return query;
    }

    #endregion

    #region Update and Delete Helper Methods

    protected void AddModificationDate(TEntity entity, DateTime? modificationDate = null)
    {
        if (entity is IHasModificationDate modified)
        {
            modified.LastModificationDate = modificationDate ?? DateTime.Now;
        }
    }

    protected void AddModificationDate(IEnumerable<TEntity> entities)
    {
        if (entities.Any() && entities.First() is IHasModificationDate)
        {
            var modificationDate = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                var modified = (IHasModificationDate)entity;
                modified.LastModificationDate = modificationDate;
            }
        }
    }

    #endregion

    #region Deletion Helper Methods

    protected void SoftDelete(TEntity entity, DateTime? deletionDate = null)
    {
        var softDeleted = (ISoftDeleted)entity;
        softDeleted.DeletionDate = deletionDate ?? DateTime.UtcNow;
        softDeleted.Deleted = true;

        if (entity is IHasModificationDate)
        {
            AddModificationDate(entity, softDeleted.DeletionDate);
        }
    }

    protected void SoftDelete(IEnumerable<TEntity> entities)
    {
        var now = DateTime.UtcNow;
        foreach (var entity in entities)
            SoftDelete(entity, now);
    }

    protected bool SoftDeleted(bool permanent, TEntity entity)
    {
        if (permanent)
            return false;

        if (entity is ISoftDeleted)
        {
            SoftDelete(entity);
            return true;
        }

        return false;
    }

    #endregion
}
