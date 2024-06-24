using AutoMapper;
using EM.Application.Features.Employees.Models;
using EM.Application.Features.Employees.Services;
using EM.Domain.Common.Paging;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace EM.Application.Features.Employees.Queries.FilterEmployee
{
    public class FilterEmployeeQueryHandler : IRequestHandler<FilterEmployeeQuery, EmployeeListModel>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        public FilterEmployeeQueryHandler(
            IMapper mapper,
            IEmployeeService employeeService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
        }

        public async Task<EmployeeListModel> Handle(FilterEmployeeQuery request, CancellationToken cancellationToken)
        {
            ISpecification<Employee> specification = new Specification<Employee>(Predicate: GetFilterExpression(request),
                                                                                 Includes: new string[] { "Department" },
                                                                                 EnableTracking: false);

            IPaginate<Employee> paginatedResult = await _employeeService.GetListAsync(specification: specification,
                                                                                      pageRequest: request.PageRequest,
                                                                                      cancellationToken: cancellationToken);

            return _mapper.Map<EmployeeListModel>(paginatedResult);
        }

        private Expression<Func<Employee, bool>> GetFilterExpression(FilterEmployeeQuery request)
        {
            /* Sql code:
             *
             *
             * select * from Employee emp 
             * where ((@name is not null and emp.Name like '%'+@name+'%') or @name is null)
                 and ((@lastName is not null and emp.LastName like '%'+@lastName+'%') or @lastName is null)
                 and ((@departmentId is not null and emp.DepartmentId = @departmentId) or @departmentId is null)
                 and ((@startBirthDate is not null and emp.BirthDate >= @startBirthDate) or @startBirthDate is null)
                 and ((@endBirthDate is not null and emp.BirthDate <= @endBirthDate) or @endBirthDate is null)
            *
            *
            */

            Expression<Func<Employee, bool>> result =
                e => ((!string.IsNullOrEmpty(request.Name) && e.Name.Contains(request.Name)) || string.IsNullOrEmpty(request.Name))
                     && ((!string.IsNullOrEmpty(request.LastName) && e.LastName.Contains(request.LastName)) || string.IsNullOrEmpty(request.LastName))
                     && ((request.DepartmentId != null && e.DepartmentId == request.DepartmentId) || request.DepartmentId == null)
                     && ((request.StartBirthDate != null && e.BirthDate >= request.StartBirthDate) || request.StartBirthDate == null)
                     && ((request.EndBirthDate != null && e.BirthDate <= request.EndBirthDate) || request.EndBirthDate == null);

            return result;
        }
    }
}
