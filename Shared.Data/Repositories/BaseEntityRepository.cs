using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Shared.Data.Repositories;

public class BaseEntityRepository<TEntity, TId> : BaseRepository<TEntity>, IBaseEntityRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    public BaseEntityRepository(DbContext context)
        : base(context)
    { }

    #region Create

    #endregion

    #region Read

    public TEntity? Get(TId id)
    {
        return set.Find(id);
    }

    public List<TEntity> Get(IEnumerable<TId> ids, string? orderBy = null)
    {
        return GetEntities(e => ids.Contains(e.Id), null, null, null, orderBy);
    }

    public async Task<TEntity?> GetAsync(TId id)
    {
        return await set.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAsync(IEnumerable<TId> ids, string? orderBy = null)
    {
        return await GetEntitiesAsync(e => ids.Contains(e.Id), null, null, null, orderBy);
    }

    #endregion

    #region Update

    #endregion

    #region Delete

    public void Delete(TId id, bool permanent = false)
    {
        var entity = Get(id);

        if (entity == null)
            return;

        if (!SoftDeleted(permanent, entity))
            set.Remove(entity);
    }

    public void Delete(IEnumerable<TId> ids, bool permanent = false)
    {
        var entities = Get(ids);

        if (entities.Count == 0)
            return;

        if (!permanent && entities.First() is ISoftDeleted)
            SoftDelete(entities);
        else
            set.RemoveRange(entities);

    }

    #endregion

    #region Creation Helper Methods

    #endregion

    #region Read Helper Methods

    #endregion

    #region Update and Delete Helper Methods

    #endregion

    #region Deletion Helper Methods

    #endregion
}
