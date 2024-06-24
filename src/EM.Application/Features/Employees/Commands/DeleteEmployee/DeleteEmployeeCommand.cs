using MediatR;

namespace EM.Application.Features.Employees.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
