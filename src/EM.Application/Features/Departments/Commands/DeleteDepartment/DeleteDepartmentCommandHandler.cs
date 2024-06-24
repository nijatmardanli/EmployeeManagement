using AutoMapper;
using EM.Application.Exceptions;
using EM.Application.Features.Departments.Services;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using MediatR;

namespace EM.Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;

        public DeleteDepartmentCommandHandler(IMapper mapper,
                                              IDepartmentService departmentService)
        {
            _mapper = mapper;
            _departmentService = departmentService;
        }

        public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            ISpecification<Department> specification = new Specification<Department>(Predicate: d => d.Id == request.Id);

            Department department = await _departmentService.GetByIdAsync(id: request.Id, specification: specification, cancellationToken: cancellationToken)
                ?? throw new NotFoundException("Department not found");

            await _departmentService.DeleteAsync(department, cancellationToken);

            return Unit.Value;
        }
    }
}
