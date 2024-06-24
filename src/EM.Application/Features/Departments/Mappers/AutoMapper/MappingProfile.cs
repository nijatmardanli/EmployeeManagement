using AutoMapper;
using EM.Application.Features.Departments.Commands.CreateDepartment;
using EM.Application.Features.Departments.Dtos;
using EM.Domain.Entities;

namespace EM.Application.Features.Departments.Mappers.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, GetDepartmentDto>().ReverseMap();

            CreateMap<CreateDepartmentCommand, Department>().ReverseMap();
            CreateMap<Department, CreatedDepartmentDto>().ReverseMap();
        }
    }
}
