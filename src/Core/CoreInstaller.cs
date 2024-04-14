using Microsoft.Extensions.DependencyInjection;
using Core.MediatrPipelines;
using Core.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Core
{
    public static class CoreInstaller
    {
        public static IServiceCollection InstallBaseApp(this IServiceCollection services)
        {
            services.AddExecutionPipeline();

            services.AddTransient<GlobalExceptionHandlerMiddleware>();

            

            return services;
        }
    }
}
