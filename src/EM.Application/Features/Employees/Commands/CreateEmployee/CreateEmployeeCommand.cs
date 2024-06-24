using EM.Application.Features.Employees.Dtos;
using MediatR;

namespace EM.Application.Features.Employees.Commands.CreateEmployee
{
    public record CreateEmployeeCommand : IRequest<CreatedEmployeeDto>
    {
        public string Name { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public DateTime BirthDate { get; set; }

        public int DepartmentId { get; set; }
    }
}
