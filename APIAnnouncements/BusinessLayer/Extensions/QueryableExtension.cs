using AutoMapper;
using BusinessLayer.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Extensions
{
    public static class QueryableExtension
    {
        public static async Task<PagedResult<T>> GetPaged<T, TKey>(this IQueryable<TKey> objects, int page, int pageSize,
            IMapper mapper, CancellationToken cancellationToken) where T : class where TKey : class
        {
            var pagedResult = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
            };
            await Task.Run(() =>
            {
                pagedResult.RowCount = objects.Count();
            }, cancellationToken);

            var pageCount = (double)pagedResult.RowCount / pageSize;
            pagedResult.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;

            pagedResult.Result = await mapper.ProjectTo<T>(objects.Skip(skip).Take(pageSize)).ToArrayAsync(cancellationToken);
            return pagedResult;
        }
        public static IQueryable<T> GetSortBy<T, TKey>(this IQueryable<T> objects, Expression<Func<T, TKey>> sortExpr, SortDirection? sortDirection)
        {
            if (sortDirection == SortDirection.Desc)
            {
                objects = objects.OrderByDescending(sortExpr);
            }
            else
            {
                objects = objects.OrderBy(sortExpr);
            }

            return objects;
        }
        public static IQueryable<T> SearchForMatches<T>(this IQueryable<T> objects, Expression<Func<T, string>> stringProperty, string searchString) where T : class
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return objects;
            }

            var isNotNullExpression = Expression.NotEqual(stringProperty.Body,
                Expression.Constant(null));
            
            var searchTermExpression = Expression.Constant(searchString);
            var checkContainsExpression = Expression.Call(stringProperty.Body,
                typeof(string).GetMethod("Contains", new[] { typeof(string) })!, searchTermExpression);
            
            var notNullAndContainsExpression = Expression.AndAlso(isNotNullExpression, checkContainsExpression);

            var methodCallExpression = Expression.Call(typeof(Queryable),
                "Where",
                new[] { objects.ElementType },
                objects.Expression,
                Expression.Lambda<Func<T, bool>>(notNullAndContainsExpression, stringProperty.Parameters));

            return objects.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
