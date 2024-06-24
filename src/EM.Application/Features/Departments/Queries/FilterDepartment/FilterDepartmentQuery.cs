using EM.Application.Features.Departments.Dtos;
using EM.Application.Features.Departments.Models;
using EM.Domain.Common.Paging;
using MediatR;

namespace EM.Application.Features.Departments.Queries.FilterDepartment
{
    public record FilterDepartmentQuery : IRequest<DepartmentListModel>
    {
        public string Name { get; set; } = default!;

        public PageRequest PageRequest { get; set; } = default!;
    }
}
