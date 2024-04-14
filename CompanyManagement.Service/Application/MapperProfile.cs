using AutoMapper;
using CompanyManagement.Contracts.Models;
using CompanyManagement.Service.Domain.DataModels;

namespace CompanyManagement.Service.Application
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CompanyDataModel, Company>()
                .ForMember(p => p.CompanyName, x => x.MapFrom(y => y.Name))
                .ReverseMap();
        }
    }
}
