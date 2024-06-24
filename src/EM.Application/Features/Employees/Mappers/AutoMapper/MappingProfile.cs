using AutoMapper;
using EM.Application.Features.Employees.Commands.CreateEmployee;
using EM.Application.Features.Employees.Dtos;
using EM.Domain.Entities;

namespace EM.Application.Features.Employees.Mappers.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEmployeeCommand, Employee>().ReverseMap();
            CreateMap<Employee, CreatedEmployeeDto>().ReverseMap();

            CreateMap<Employee, UpdatedEmployeeDto>().ReverseMap();

            CreateMap<Employee, GetEmployeeDto>().ReverseMap();
        }
    }
}
