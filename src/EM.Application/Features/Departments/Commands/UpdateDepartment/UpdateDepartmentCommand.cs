using EM.Application.Features.Departments.Dtos;
using MediatR;

namespace EM.Application.Features.Departments.Commands.UpdateDepartment
{
    public record UpdateDepartmentCommand : IRequest<UpdatedDepartmentDto>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
