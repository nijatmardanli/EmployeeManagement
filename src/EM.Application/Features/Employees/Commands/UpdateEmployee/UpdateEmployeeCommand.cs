using EM.Application.Features.Employees.Dtos;
using MediatR;

namespace EM.Application.Features.Employees.Commands.UpdateEmployee
{
    public record UpdateEmployeeCommand : IRequest<UpdatedEmployeeDto>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public DateTime BirthDate { get; set; }

        public int DepartmentId { get; set; }
    }

}
