namespace EM.Application.Features.Employees.Dtos
{
    public record CreatedEmployeeDto(int Id, string Name, string LastName, DateTime BirthDate, int DepartmentId);
}
