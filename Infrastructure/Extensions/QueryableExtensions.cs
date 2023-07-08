using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class QueryableExtensions
{
    // Adding Includes to Query
    public static IQueryable<TEntity> GetIncludeQuery<TEntity>(this IQueryable<TEntity> query, params string[] includes) where TEntity : class, IBaseEntity
    {
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
        return query;
    }
}
