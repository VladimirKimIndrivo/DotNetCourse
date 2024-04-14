using Asp.Versioning;
using CompanyManagement.Routing;
using CompanyManagement.Service;
using Core;
using Core.Middleware;
using SwaggerHierarchySupport;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

builder.Services.AddApiVersioning(opts =>
    {
        opts.DefaultApiVersion = new ApiVersion(1.0);
        opts.AssumeDefaultVersionWhenUnspecified = true;
        opts.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(opts =>
    {
        opts.GroupNameFormat = "'v'VVV";
        opts.SubstituteApiVersionInUrl = true;
    })
    .EnableApiVersionBinding();

builder.Services.InstallBaseApp();

//Add services
builder.Services.AddCompanyManagementService(builder.Configuration);

var app = builder.Build();

//Apply migrations
app.ApplyCompanyManagementMigrations();

//Add endpoints
app.UseCompanyManagementEndpoints();

app.UseSwagger();
app.UseSwaggerUI(c =>
{

    foreach (var groupName in app.DescribeApiVersions().Select(d => d.GroupName))
    {
        c.DisplayRequestDuration();
        c.AddHierarchySupport();
        c.DocExpansion(DocExpansion.None);
        c.SwaggerEndpoint(
            url: $"/swagger/{groupName}/swagger.json",
            name: groupName);
    }
    c.RoutePrefix = string.Empty;
});

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.Run();
