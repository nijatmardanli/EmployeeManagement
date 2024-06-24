using EM.Application.Features.Departments.Dtos;
using MediatR;

namespace EM.Application.Features.Departments.Commands.CreateDepartment
{
    public record CreateDepartmentCommand : IRequest<CreatedDepartmentDto>
    {
        public string Name { get; set; } = default!;
    }
}
