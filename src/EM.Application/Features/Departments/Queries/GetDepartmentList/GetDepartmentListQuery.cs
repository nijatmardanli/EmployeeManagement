using EM.Application.Features.Departments.Dtos;
using MediatR;

namespace EM.Application.Features.Departments.Queries.GetDepartmentList
{
    public record GetDepartmentListQuery : IRequest<IReadOnlyList<GetDepartmentDto>>
    {
    }
}
