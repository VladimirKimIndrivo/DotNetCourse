using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CompanyManagement.Contracts.Commands
{
    public class CreateCompanyCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = "";
    }   

    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Company is required")
                .MaximumLength(5)
                .WithMessage("Name cannot be more than 5 characters long.");    
        }
    }
}
