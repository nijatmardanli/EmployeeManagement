using AutoMapper;
using EM.Application.Features.Departments.Dtos;
using EM.Application.Features.Departments.Services;
using EM.Domain.Entities;
using MediatR;

namespace EM.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, CreatedDepartmentDto>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;

        public CreateDepartmentCommandHandler(IMapper mapper,
                                              IDepartmentService departmentService)
        {
            _mapper = mapper;
            _departmentService = departmentService;
        }

        public async Task<CreatedDepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department = _mapper.Map<Department>(request);
            Department createdDepartment = await _departmentService.AddAsync(department, cancellationToken);

            CreatedDepartmentDto result = _mapper.Map<CreatedDepartmentDto>(createdDepartment);
            return result;
        }
    }
}
