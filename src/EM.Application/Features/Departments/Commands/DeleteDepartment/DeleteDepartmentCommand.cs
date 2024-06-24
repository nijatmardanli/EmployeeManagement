using MediatR;

namespace EM.Application.Features.Departments.Commands.DeleteDepartment
{
    public record DeleteDepartmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
