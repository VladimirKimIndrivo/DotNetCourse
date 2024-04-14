namespace Core.Models
{
    public abstract class QueryParams
    {
        protected QueryParams(
            string searchValue,
            string sortOrder,
            string sortField,
            int pageSize,
            int pageNumber)
        {   
            SortOrder = sortOrder ?? "asc";
            SortField = sortField;
            OrderBy = $"{SortField} {SortOrder}";
            PageSize = pageSize <= 0 ? 20 : pageSize;
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            SkipPageCount = (PageNumber - 1) * PageSize;
        }

        public string SortOrder { get; }
        public string SortField { get; }
        public string OrderBy { get; }
        public int PageSize { get; }
        public int PageNumber { get; }

        public int SkipPageCount { get; }
    }
}
