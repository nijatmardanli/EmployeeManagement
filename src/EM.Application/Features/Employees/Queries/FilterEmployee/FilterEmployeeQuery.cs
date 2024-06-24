using EM.Application.Features.Employees.Models;
using EM.Domain.Common.Paging;
using MediatR;

namespace EM.Application.Features.Employees.Queries.FilterEmployee
{
    public record FilterEmployeeQuery : IRequest<EmployeeListModel>
    {
        public string? Name { get; set; }

        public string? LastName { get; set; }

        public DateTime? StartBirthDate { get; set; }

        public DateTime? EndBirthDate { get; set; }

        public int? DepartmentId { get; set; }

        public PageRequest PageRequest { get; set; } = default!;
    }
}
