using AutoMapper;
using PumoxWebApplication.DTOs;
using PumoxWebApplication.Models;

namespace PumoxWebApplication.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyDTO, Company>()
                .ReverseMap();
            CreateMap<EmployeeDTO, Employee>()
                .ReverseMap();
        }
    }
}
