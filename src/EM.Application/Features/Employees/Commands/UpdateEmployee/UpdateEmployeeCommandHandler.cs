using AutoMapper;
using EM.Application.Exceptions;
using EM.Application.Features.Employees.Dtos;
using EM.Application.Features.Employees.Rules;
using EM.Application.Features.Employees.Services;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using MediatR;

namespace EM.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdatedEmployeeDto>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly EmployeeBusinessRules _employeeBusinessRules;

        public UpdateEmployeeCommandHandler(
            IMapper mapper,
            IEmployeeService employeeService,
            EmployeeBusinessRules employeeBusinessRules)
        {
            _mapper = mapper;
            _employeeService = employeeService;
            _employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<UpdatedEmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeBusinessRules.DepartmentMustBeExistAsync(request.DepartmentId, cancellationToken);

            ISpecification<Employee> specification = new Specification<Employee>(Predicate: e => e.Id == request.Id, Includes: new string[] { "Department" });
            Employee employee = await _employeeService.GetByIdAsync(id: request.Id, specification, cancellationToken: cancellationToken)
                ?? throw new NotFoundException("Employee not found");

            UpdateEmployeeDetails(request, employee);

            Employee updatedEmployee = await _employeeService.UpdateAsync(employee, cancellationToken);

            UpdatedEmployeeDto result = _mapper.Map<UpdatedEmployeeDto>(updatedEmployee);
            return result;
        }

        private void UpdateEmployeeDetails(UpdateEmployeeCommand request, Employee employee)
        {
            employee.Name = request.Name;
            employee.LastName = request.LastName;
            employee.BirthDate = request.BirthDate;
            employee.DepartmentId = request.DepartmentId;
        }
    }
}
