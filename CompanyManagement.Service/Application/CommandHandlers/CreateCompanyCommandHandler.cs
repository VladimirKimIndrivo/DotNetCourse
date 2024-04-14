using CompanyManagement.Contracts.Commands;
using CompanyManagement.Service.Domain;
using CompanyManagement.Service.Domain.DataModels;
using FluentResults;
using MediatR;

namespace CompanyManagement.Service.Application.CommandHandlers
{
    internal class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Result<Guid>>
    {   
        private readonly ICompanyManagementDbContext _context;

        public CreateCompanyCommandHandler(ICompanyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new CompanyDataModel()
            {
                Name = request.Name
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok(company.Id);   
        }
    }
}
