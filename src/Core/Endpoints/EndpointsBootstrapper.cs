using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace Core.Endpoints
{
    public static class EndpointsBootstrapper
    {
        public static void UseEndpoints<TMarker>(this IApplicationBuilder app)
        {
            app.UseEndpoints(typeof(TMarker).Assembly);
        }

        public static void UseEndpoints(this IApplicationBuilder app, Assembly assembly)
        {
            var endpointTypes = GetEndpointDefinitionsFromAssembly(assembly);

            foreach (var endpointType in endpointTypes)
            {
                endpointType.GetMethod(nameof(IEndpointsDefinition.ConfigureEndpoints))!
                    .Invoke(null, new object[]
                    {
                    app
                    });
            }
        }

        private static IEnumerable<TypeInfo> GetEndpointDefinitionsFromAssembly(Assembly assembly)
        {
            var endpointDefinitions =
                assembly
                    .DefinedTypes
                    .Where(x => x is
                    {
                        IsAbstract: false,
                        IsInterface: false
                    }
                                && typeof(IEndpointsDefinition).IsAssignableFrom(x));

            return endpointDefinitions;
        }
    }
}
