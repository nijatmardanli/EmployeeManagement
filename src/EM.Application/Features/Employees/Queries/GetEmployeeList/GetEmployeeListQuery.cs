using EM.Application.Features.Employees.Dtos;
using MediatR;

namespace EM.Application.Features.Employees.Queries.GetEmployeeList
{
    public record GetEmployeeListQuery : IRequest<IReadOnlyList<GetEmployeeDto>>
    {
    }
}
