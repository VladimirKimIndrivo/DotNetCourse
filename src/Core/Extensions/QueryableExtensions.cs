using Core.Models;
using System.Linq.Dynamic.Core;

namespace Core.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        ///     Apply Order based on request params
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Queryable Source</param>
        /// <param name="queryParams">Query params from request</param>
        /// <returns></returns>
        public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> source, QueryParams queryParams)
        {
            if (!string.IsNullOrEmpty(queryParams.SortField))
                source = source.OrderBy(queryParams.OrderBy);
            return source;
        }
    }
}

