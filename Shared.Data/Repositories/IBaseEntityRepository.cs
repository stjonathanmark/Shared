using System.Linq.Expressions;

namespace Shared.Data.Repositories
{
    public interface IBaseEntityRepository<TEntity, TId> : IBaseRepository<TEntity>
        where TId : struct
        where TEntity : BaseEntity<TId>
    {
        #region Create

        #endregion

        #region Read

        TEntity? Get(TId id);

        List<TEntity> Get(IEnumerable<TId> ids, string? orderBy = null);

        Task<TEntity?> GetAsync(TId id);

        Task<List<TEntity>> GetAsync(IEnumerable<TId> ids, string? orderBy = null);

        #endregion

        #region Update

        #endregion

        #region Delete

        void Delete(TId id, bool permanent = false);

        void Delete(IEnumerable<TId> ids, bool permanent = false);

        #endregion
    }
}
