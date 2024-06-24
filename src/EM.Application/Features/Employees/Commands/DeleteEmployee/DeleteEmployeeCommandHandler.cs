using EM.Application.Exceptions;
using EM.Application.Features.Employees.Services;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using MediatR;

namespace EM.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly IEmployeeService _employeeService;

        public DeleteEmployeeCommandHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            ISpecification<Employee> specification = new Specification<Employee>(Predicate: e => e.Id == request.Id);

            Employee employee = await _employeeService.GetByIdAsync(id: request.Id, specification: specification, cancellationToken: cancellationToken)
                ?? throw new NotFoundException("Employee not found!");

            await _employeeService.DeleteAsync(employee, cancellationToken);

            return Unit.Value;
        }
    }
}
