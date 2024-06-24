namespace EM.Application.Features.Employees.Dtos
{
    public record UpdatedEmployeeDto(int Id, string Name, string LastName, DateTime BirthDate, int DepartmentId);
}
