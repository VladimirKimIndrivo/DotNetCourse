using CompanyManagement.Contracts.Models;
using FluentResults;
using FluentValidation;
using MediatR;

namespace CompanyManagement.Contracts.Queries
{
    public class GetCompanyQuery : IRequest<Result<Company>>
    {
        public Guid Id { get; set; }

        public GetCompanyQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetCompanyQueryValidator : AbstractValidator<GetCompanyQuery>
    {
        public GetCompanyQueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("The Id must not be the default GUID value.");
        }
    }
}
