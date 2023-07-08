using Application.Abstracts.Repositories.Base;
using Domain.Entities.Base;
using Infrastructure.Extensions;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Infrastructure.Concretes.Repositories.Base;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
{
    private readonly MapDbContext _context;

    public Repository(MapDbContext context)
    {
        _context = context;
    }

    public DbSet<TEntity> Table => _context.Set<TEntity>();
    public async Task<TEntity?> GetAsync(string id, bool tracking = true, params string[] includes)
    {
        IQueryable<TEntity> query = Table.GetIncludeQuery(includes);
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? predicate = null, bool tracking = true, params string[] includes)
    {

        IQueryable<TEntity> query = Table.GetIncludeQuery(includes);
        if (!tracking)
            query = query.AsNoTracking();
        return predicate is null
            ? await query.FirstOrDefaultAsync()
            : await query.Where(predicate).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, bool tracking = true, params string[] includes)
    {
        IQueryable<TEntity> query = Table.GetIncludeQuery(includes);
        if (!tracking)
            query = query.AsNoTracking();
        return predicate is null
            ? await query.ToListAsync()
            : await query.Where(predicate).ToListAsync();
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync(int page, int size, Expression<Func<TEntity, bool>>? predicate = null, bool tracking = true, params string[] includes)
    {
        IQueryable<TEntity> query = Table.GetIncludeQuery(includes).Skip((page - 1) * size).Take(size);
        if (!tracking)
            query = query.AsNoTracking();
        return predicate is null
            ? await query.ToListAsync()
            : await query.Where(predicate).ToListAsync();
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync<TOrderBy>(int? page, int? size, Expression<Func<TEntity, TOrderBy>> orderBy, bool isOrderBy = true, Expression<Func<TEntity, bool>>? predicate = null, bool tracking = true, params string[] includes)
    {
        IQueryable<TEntity> query = (page == null || size == null) ? Table.GetIncludeQuery(includes)
            : Table.GetIncludeQuery(includes).Skip(((int)page - 1) * (int)size).Take((int)size);

        if (!tracking)
            query = query.AsNoTracking();

        if (predicate != null)
            query = query.Where(predicate);

        if (isOrderBy)
            query = query.OrderBy(orderBy);
        else
            query = query.OrderByDescending(orderBy);

        return await query.ToListAsync();
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        EntityEntry<TEntity> entry = await Table.AddAsync(entity);
        entry.State = EntityState.Added;
        return entry.Entity;
    }

    public TEntity Remove(TEntity entity)
    {
        EntityEntry<TEntity> entry = Table.Remove(entity);
        entry.State = EntityState.Deleted;
        return entry.Entity;
    }
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            EntityEntry<TEntity> entry = Table.Remove(entity);
            entry.State = EntityState.Deleted;
        }
    }
    public void Update(TEntity entity)
    {
        EntityEntry<TEntity> entry = Table.Update(entity);
        entry.State = EntityState.Modified;
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await Table.AddRangeAsync(entities);
    }
}
