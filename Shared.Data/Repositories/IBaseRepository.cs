using System.Linq.Expressions;

namespace Shared.Data.Repositories;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    int EntitiesFound { get; set; }

    #region Create

    void Create(TEntity entity);

    void Create(IEnumerable<TEntity> entities);

    Task CreateAsync(TEntity entity);

    Task CreateAsync(IEnumerable<TEntity> entities);

    #endregion

    #region Read

    List<TEntity> Get(string? orderBy = null, int? skip = null, int? take = null, IEnumerable<string>? includes = null, bool includeDeleted = false);

    TEntity? Get(params object[] id);

    List<TEntity> Get(Expression<Func<TEntity, bool>> predicate, string? orderBy = null, int? pageNumer = null, int? pageSize = null, IEnumerable<string>? includes = null, bool includeDeleted = false);

    List<TEntity> Get(string filter, string? orderBy = null, int? pageNumer = null, int? pageSize = null, IEnumerable<string>? includes = null, bool includeDeleted = false);

    Task<List<TEntity>> GetAsync(string? orderBy = null, int? skip = null, int? take = null, IEnumerable<string>? includes = null, bool includeDeleted = false);

    Task<TEntity?> GetAsync(params object[] id);

    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, string? orderBy = null, int? pageNumer = null, int? pageSize = null, IEnumerable<string>? includes = null, bool includeDeleted = false);

    Task<List<TEntity>> GetAsync(string filter, string? orderBy = null, int? pageNumer = null, int? pageSize = null, IEnumerable<string>? includes = null, bool includeDeleted = false);

    #endregion

    #region Update

    void Update(TEntity entity);

    void Update(IEnumerable<TEntity> entities);

    #endregion

    #region Delete

    void Delete(TEntity entity, bool permanent = false);

    void Delete(IEnumerable<TEntity> entities, bool permanent = false);

    #endregion
}
