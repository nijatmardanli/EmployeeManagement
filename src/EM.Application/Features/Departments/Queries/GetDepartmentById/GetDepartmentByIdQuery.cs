using EM.Application.Features.Departments.Dtos;
using MediatR;

namespace EM.Application.Features.Departments.Queries.GetDepartmentById
{
    public record GetDepartmentByIdQuery : IRequest<GetDepartmentDto?>
    {
        public int Id { get; set; }
    }
}
