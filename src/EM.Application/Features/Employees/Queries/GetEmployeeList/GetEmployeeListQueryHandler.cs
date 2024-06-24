using AutoMapper;
using EM.Application.Features.Employees.Dtos;
using EM.Application.Features.Employees.Services;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using MediatR;

namespace EM.Application.Features.Employees.Queries.GetEmployeeList
{
    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, IReadOnlyList<GetEmployeeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        public GetEmployeeListQueryHandler(
            IMapper mapper,
            IEmployeeService employeeService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
        }

        public async Task<IReadOnlyList<GetEmployeeDto>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            ISpecification<Employee> specification = new Specification<Employee>(Includes: new string[] { "Department" },
                                                                                 EnableTracking: false);

            IReadOnlyList<Employee> employees = await _employeeService.GetListAsync(specification,
                                                                                    cancellationToken: cancellationToken);

            return _mapper.Map<IReadOnlyList<GetEmployeeDto>>(employees);
        }
    }
}
