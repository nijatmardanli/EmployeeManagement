using AutoMapper;
using EM.Application.Features.Departments.Commands.CreateDepartment;
using EM.Application.Features.Departments.Commands.UpdateDepartment;
using EM.Application.Features.Departments.Dtos;
using EM.Application.Features.Departments.Models;
using EM.Domain.Common.Paging;
using EM.Domain.Entities;

namespace EM.Application.Features.Departments.Mappers.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, GetDepartmentDto>().ReverseMap();

            CreateMap<IPaginate<Department>, DepartmentListModel>().ReverseMap();

            CreateMap<CreateDepartmentCommand, Department>().ReverseMap();
            CreateMap<Department, CreatedDepartmentDto>().ReverseMap();

            CreateMap<UpdateDepartmentCommand, Department>().ReverseMap();
            CreateMap<Department, UpdatedDepartmentDto>().ReverseMap();
        }
    }
}
