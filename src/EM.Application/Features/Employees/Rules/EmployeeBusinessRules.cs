using EM.Application.Exceptions;
using EM.Application.Features.Departments.Services;

namespace EM.Application.Features.Employees.Rules
{
    public class EmployeeBusinessRules
    {
        private readonly IDepartmentService _departmentService;

        public EmployeeBusinessRules(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task DepartmentMustBeExistAsync(int departmentId, CancellationToken cancellationToken)
        {
            bool exists = await _departmentService.AnyAsync(d => d.Id == departmentId, cancellationToken);

            if (!exists)
                throw new NotFoundException("Department not found!");
        }
    }
}
