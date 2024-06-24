using EM.Application.Features.Departments.Dtos;

namespace EM.Application.Features.Employees.Dtos
{
    public record GetEmployeeDto(
        int Id,
        string Name,
        string LastName,
        DateTime BirthDate,
        GetDepartmentDto Department);
}
