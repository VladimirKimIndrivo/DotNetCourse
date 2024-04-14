using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Endpoints;

namespace CompanyManagement.Routing
{
    public static class CompanyManagementEndpointsInstaller
    {
        public static IApplicationBuilder UseCompanyManagementEndpoints(this IApplicationBuilder app)
        {   
            app.UseEndpoints<CompanyManagementEndpoints>();

            return app;
        }   
    }
}
