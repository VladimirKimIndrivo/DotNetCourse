namespace Core.Models;

public class QueryResult<T>
{
    public IEnumerable<T> Data { get; }
    public int TotalCount { get; }

    public QueryResult(IEnumerable<T> data, int totalCount)
    {
        Data = data;
        TotalCount = totalCount;
    }
}