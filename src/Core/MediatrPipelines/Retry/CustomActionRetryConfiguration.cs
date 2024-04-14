namespace Core.MediatrPipelines.Retry
{   
    public class CustomActionRetryConfiguration
    {
        public string Name { get; set; } = string.Empty;

        public int RetryCount { get; set; }

        public int IncrementalCount { get; set; }
    }
}