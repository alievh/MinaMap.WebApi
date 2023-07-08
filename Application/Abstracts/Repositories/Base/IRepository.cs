using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Abstracts.Repositories.Base;

public interface IRepository<TEntity> where TEntity : class, IBaseEntity
{
    DbSet<TEntity> Table { get; }

    Task<TEntity?> GetAsync(string id,
        bool tracking = true,
        params string[] include);

    Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool tracking = true,
        params string[] includeProperties);

    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool tracking = true,
        params string[] includes);

    Task<IEnumerable<TEntity>> GetAllAsync(
       int page,
       int size,
       Expression<Func<TEntity, bool>>? predicate = null,
       bool tracking = true,
       params string[] includes);

    Task<IEnumerable<TEntity>> GetAllAsync<TOrderBy>(
        int? page,
        int? size,
        Expression<Func<TEntity, TOrderBy>> orderBy,
        bool isOrderBy = true,
        Expression<Func<TEntity, bool>>? predicate = null,
        bool tracking = true,
        params string[] includes);

    Task<TEntity> AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void RemoveRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    TEntity Remove(TEntity entity);
}
