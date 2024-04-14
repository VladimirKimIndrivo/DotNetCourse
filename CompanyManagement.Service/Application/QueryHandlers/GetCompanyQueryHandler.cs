using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompanyManagement.Contracts.Models;
using CompanyManagement.Contracts.Queries;
using CompanyManagement.Service.Domain;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Service.Application.QueryHandlers
{
    internal class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, Result<Company>>
    {
        private readonly ICompanyManagementDbContext _context;
        private readonly IMapper _mapper;

        public GetCompanyQueryHandler(ICompanyManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Company>> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            /*var company = await _context.Companies
                .Where(p => p.Id == request.Id)
                .ProjectTo<Company>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);*/

            var company = await _context.Companies
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

            //business logic

            if (company == null)
            {
                return Result.Fail("Company not found");
            }

            var companyDto = _mapper.Map<Company>(company); 
            
            return Result.Ok(companyDto);  
        }
    }
}
