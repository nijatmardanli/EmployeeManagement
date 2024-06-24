using EM.Application.Features.Employees.Dtos;
using EM.Domain.Common.Paging;

namespace EM.Application.Features.Employees.Models
{
    public record EmployeeListModel : BasePageableModel
    {
        public IReadOnlyList<GetEmployeeDto> Items { get; set; } = new List<GetEmployeeDto>();
    }
}
