using Core.MediatrPipelines.Retry;

namespace Core.MediatR.Retry
{
    public class RetryMiddlewareOptions
    {
        public int DefaultOperationRetryCount { get; set; } = 4;

        public int DefaultOperationIncrementalCount { get; set; } = 10;

        public List<CustomActionRetryConfiguration> CustomConfiguration { get; set; } = new List<CustomActionRetryConfiguration>();
    }
}