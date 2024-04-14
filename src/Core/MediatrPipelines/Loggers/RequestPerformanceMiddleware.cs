using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.MediatrPipelines.Loggers
{   
    public class RequestPerformanceMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer = new();

        private readonly ILogger<TRequest> _logger;

        public RequestPerformanceMiddleware(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.LogInformation("Executing request: '{Name}' '{Request}'", name, request);

            _timer.Start();

            var response = await next();

            _timer.Stop();

            _logger.LogInformation("Finishing request: '{Name}' '{Request}'", name, request);

            if (_timer.ElapsedMilliseconds <= 5000) return response;

            _logger.LogWarning("Long Running Request: '{Name}' ({ElapsedTime} milliseconds) '{Request}'", name, _timer.ElapsedMilliseconds, request);

            return response;
        }
    }
}