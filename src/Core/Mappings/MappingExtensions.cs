using Core.Models;

namespace Core.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PagedList<TDestination>> ToPagedListAsync<TDestination>(
            this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        {
            return PagedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
        }
    }
}
