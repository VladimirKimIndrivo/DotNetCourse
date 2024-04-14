using Core.MediatrPipelines.Loggers;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace Core.MediatrPipelines
{
    public static class MediatrPipelineInstaller
    {
        public static void AddExecutionPipeline(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceMiddleware<,>));
        }   
    }
}