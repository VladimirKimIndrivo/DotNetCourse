using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Core.Models
{
    public class PagedList<T>
    {
        [JsonProperty("items")] 
        public List<T> Items { get; }

        [JsonProperty("pageIndex")] 
        public int PageIndex { get; }

        [JsonProperty("totalPages")] 
        public int TotalPages { get; }

        [JsonProperty("totalCount")] 
        public int TotalCount { get; }

        [JsonProperty("hasPreviousPage")] 
        public bool HasPreviousPage => PageIndex > 1;

        [JsonProperty("hasNextPage")] 
        public bool HasNextPage => PageIndex < TotalPages;

        public PagedList()
        {
            Items = new List<T>();
        }

        public PagedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
