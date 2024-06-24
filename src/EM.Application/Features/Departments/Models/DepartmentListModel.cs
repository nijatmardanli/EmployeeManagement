using EM.Application.Features.Departments.Dtos;
using EM.Domain.Common.Paging;

namespace EM.Application.Features.Departments.Models
{
    public record DepartmentListModel : BasePageableModel
    {
        public IReadOnlyList<GetDepartmentDto> Items { get; set; } = new List<GetDepartmentDto>();
    }
}
