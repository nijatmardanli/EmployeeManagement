using AutoMapper;
using EM.Application.Exceptions;
using EM.Application.Features.Departments.Dtos;
using EM.Application.Features.Departments.Services;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using MediatR;

namespace EM.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, UpdatedDepartmentDto>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;

        public UpdateDepartmentCommandHandler(IMapper mapper,
                                              IDepartmentService departmentService)
        {
            _mapper = mapper;
            _departmentService = departmentService;
        }

        public async Task<UpdatedDepartmentDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            ISpecification<Department> specification = new Specification<Department>(Predicate: d => d.Id == request.Id);

            Department department = await _departmentService.GetByIdAsync(id: request.Id, specification: specification, cancellationToken: cancellationToken)
                ?? throw new NotFoundException("Department not found");

            department.Name = request.Name;

            Department updatedDepartment = await _departmentService.UpdateAsync(department, cancellationToken);

            UpdatedDepartmentDto result = _mapper.Map<UpdatedDepartmentDto>(updatedDepartment);
            return result;
        }
    }
}
