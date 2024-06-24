using AutoMapper;
using EM.Application.Features.Departments.Dtos;
using EM.Application.Features.Departments.Services;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using MediatR;

namespace EM.Application.Features.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, GetDepartmentDto?>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;

        public GetDepartmentByIdQueryHandler(IMapper mapper,
                                             IDepartmentService departmentService)
        {
            _mapper = mapper;
            _departmentService = departmentService;
        }

        public async Task<GetDepartmentDto?> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            ISpecification<Department> specification = new Specification<Department>(Predicate: d => d.Id == request.Id,
                                                                                     EnableTracking: false);

            Department? department = await _departmentService.GetByIdAsync(id: request.Id,
                                                                           specification: specification,
                                                                           cancellationToken: cancellationToken);

            return _mapper.Map<GetDepartmentDto?>(department);
        }
    }
}
