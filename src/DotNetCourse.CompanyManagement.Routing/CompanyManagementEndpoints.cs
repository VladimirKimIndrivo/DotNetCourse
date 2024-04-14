using Asp.Versioning;
using CompanyManagement.Contracts;
using CompanyManagement.Contracts.Commands;
using CompanyManagement.Contracts.Models;
using CompanyManagement.Contracts.Queries;
using Core.Endpoints;
using Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using O9d.AspNet.FluentValidation;

namespace CompanyManagement.Routing
{
    internal class CompanyManagementEndpoints : IEndpointsDefinition
    {
        public static void ConfigureEndpoints(IEndpointRouteBuilder app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1.0))
                .Build();

            var group = app.MapGroup("api/v{version:apiVersion}/companies")
                .WithTags("CompanyManagement: Companies")
                .WithOpenApi()
                .WithValidationFilter()
                .WithApiVersionSet(versionSet);

            group.MapPost("/", CreateCompanyAsync)
                .Accepts<CreateCompanyCommand>(MediaTypes.ApplicationJson)
                .Produces(201)
                .ProducesValidationProblem()
                .WithName("CreateCompany")
                .WithSummary("Create new company")
                .WithDescription("This endpoint is used to create new company");

            group.MapGet("/{id:Guid}", GetCompanyByIdAsync)
                .Produces(404)
                .Produces<Company>(200)
                .WithName("GetCompany")
                .WithSummary("Get company")
                .WithDescription("This endpoint is used to get company by id");
        }

        private static async Task<IResult> CreateCompanyAsync(
            [Validate] CreateCompanyCommand command,
            IMediator mediator)
        {

            var operation = await mediator.Send(command);

            return !operation.IsSuccess 
                ? operation.ToProblemDetails() 
                : Results.Ok(operation.Value);
        }

        private static async Task<IResult> GetCompanyByIdAsync(
            Guid id,    
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var operation = await mediator.Send(new GetCompanyQuery(id), cancellationToken);

            if (!operation.IsSuccess)
            {
                return operation.ToProblemDetails();
            }

            return Results.Ok(operation.Value);
        }
    }
}
