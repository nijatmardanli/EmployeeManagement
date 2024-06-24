using AutoMapper;
using EM.Application.Features.Departments.Models;
using EM.Application.Features.Departments.Services;
using EM.Domain.Common.Paging;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using MediatR;

namespace EM.Application.Features.Departments.Queries.FilterDepartment
{
    public class FilterDepartmentQueryHandler : IRequestHandler<FilterDepartmentQuery, DepartmentListModel>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;

        public FilterDepartmentQueryHandler(IMapper mapper, IDepartmentService departmentService)
        {
            _mapper = mapper;
            _departmentService = departmentService;
        }

        public async Task<DepartmentListModel> Handle(FilterDepartmentQuery request, CancellationToken cancellationToken)
        {
            ISpecification<Department> specification = new Specification<Department>(Predicate: d => d.Name.Contains(request.Name),
                                                                                     EnableTracking: false);

            IPaginate<Department> paginatedResult = await _departmentService.GetListAsync(specification: specification,
                                                                                          pageRequest: request.PageRequest,
                                                                                          cancellationToken: cancellationToken);

            return _mapper.Map<DepartmentListModel>(paginatedResult);
        }
    }
}
