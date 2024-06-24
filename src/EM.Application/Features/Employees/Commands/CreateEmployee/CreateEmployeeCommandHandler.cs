using AutoMapper;
using EM.Application.Features.Employees.Dtos;
using EM.Application.Features.Employees.Rules;
using EM.Application.Features.Employees.Services;
using EM.Domain.Entities;
using MediatR;

namespace EM.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreatedEmployeeDto>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly EmployeeBusinessRules _employeeBusinessRules;

        public CreateEmployeeCommandHandler(
            IMapper mapper,
            IEmployeeService employeeService,
            EmployeeBusinessRules employeeBusinessRules)
        {
            _mapper = mapper;
            _employeeService = employeeService;
            _employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<CreatedEmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeBusinessRules.DepartmentMustBeExistAsync(request.DepartmentId, cancellationToken);

            Employee employee = _mapper.Map<Employee>(request);
            Employee createdEmployee = await _employeeService.AddAsync(employee, cancellationToken);

            CreatedEmployeeDto result = _mapper.Map<CreatedEmployeeDto>(createdEmployee);
            return result;
        }
    }
}
