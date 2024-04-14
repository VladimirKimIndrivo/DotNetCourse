using System.Collections;
using Core.MediatR.Retry;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;

namespace Core.MediatrPipelines.Retry
{
    public class RequestRetryMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        private readonly RetryMiddlewareOptions _config;

        public RequestRetryMiddleware(ILogger<TRequest> logger, IOptions<RetryMiddlewareOptions> config)
        {
            _logger = logger;
            _config = config.Value;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!((IList)typeof(TRequest).GetInterfaces()).Contains(typeof(IRetryMarker)))
            {
                return await next();
            }

            if (_config.CustomConfiguration is not null)
            {
                var customConfiguration = _config.CustomConfiguration.FirstOrDefault(x => x.Name == typeof(TRequest).Name);
                if (customConfiguration is not null)
                {
                    _config.DefaultOperationIncrementalCount = customConfiguration.IncrementalCount;
                    _config.DefaultOperationRetryCount = customConfiguration.RetryCount;
                }
            }

            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(retryCount: _config.DefaultOperationRetryCount, sleepDurationProvider: retryAttempt =>
                {
                    var timeToWait = TimeSpan.FromSeconds(retryAttempt * _config.DefaultOperationIncrementalCount);
                    _logger.LogTrace(
                        $"Execution delay of request '{JsonConvert.SerializeObject(request)}' for '{timeToWait.TotalSeconds}' seconds...");
                    return timeToWait;
                },
                    onRetry: (exception, pollyRetryCount, context) =>
                    {
                        if (exception != null)
                        {
                            _logger.LogError(exception, exception.Message);
                        }
                    });

            return await retryPolicy.ExecuteAsync(async () => await next());
        }
    }
}