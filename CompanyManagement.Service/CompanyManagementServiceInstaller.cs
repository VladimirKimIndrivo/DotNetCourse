using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CompanyManagement.Service.Application;
using CompanyManagement.Service.Application.CommandHandlers;
using CompanyManagement.Service.Domain;
using CompanyManagement.Service.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManagement.Service
{
    public static class CompanyManagementServiceInstaller
    {
        public static IServiceCollection AddCompanyManagementService(
            this IServiceCollection services,   
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<CompanyManagementDbContext>(x =>
            {
                x.UseSqlServer(connectionString, options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    options.MigrationsHistoryTable($"__{nameof(CompanyManagementDbContext)}");
                });
            });

            services.AddMediatR(x => { x.RegisterServicesFromAssembly(typeof(CreateCompanyCommandHandler).Assembly); });

            services.AddScoped<ICompanyManagementDbContext, CompanyManagementDbContext>();
            services.AddAutoMapper(typeof(MapperProfile).Assembly);

            return services;
        }

        public static IApplicationBuilder ApplyCompanyManagementMigrations(this IApplicationBuilder app)
        {   
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<CompanyManagementDbContext>();
            context?.Database.Migrate();
            return app;
        }
    }
}
