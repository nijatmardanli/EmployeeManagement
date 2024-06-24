using AutoMapper;
using EM.Application.Features.Employees.Dtos;
using EM.Application.Features.Employees.Services;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using MediatR;

namespace EM.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeDto>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        public GetEmployeeByIdQueryHandler(IMapper mapper, IEmployeeService employeeService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
        }

        public async Task<GetEmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            ISpecification<Employee> specification = new Specification<Employee>(Predicate: e => e.Id == request.Id,
                                                                                 Includes: new string[] { "Department" },
                                                                                 EnableTracking: false);

            Employee? employee = await _employeeService.GetByIdAsync(id: request.Id,
                                                                     specification: specification,
                                                                     cancellationToken: cancellationToken);

            return _mapper.Map<GetEmployeeDto>(employee);
        }
    }
}
