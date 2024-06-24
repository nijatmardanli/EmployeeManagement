using AutoMapper;
using EM.Application.Features.Departments.Dtos;
using EM.Application.Features.Departments.Services;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using MediatR;

namespace EM.Application.Features.Departments.Queries.GetDepartmentList
{
    public class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentListQuery, IReadOnlyList<GetDepartmentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;

        public GetDepartmentListQueryHandler(
            IMapper mapper,
            IDepartmentService departmentService)
        {
            _mapper = mapper;
            _departmentService = departmentService;
        }

        public async Task<IReadOnlyList<GetDepartmentDto>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            ISpecification<Department> specification = new Specification<Department>(EnableTracking: false);

            IReadOnlyList<Department> departments = await _departmentService.GetListAsync(specification: specification,
                                                                                          cancellationToken: cancellationToken);

            return _mapper.Map<IReadOnlyList<GetDepartmentDto>>(departments);
        }
    }
}
