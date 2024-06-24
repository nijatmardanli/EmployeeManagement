using EM.Domain.Common.Paging;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using EM.Domain.Repositories;
using EM.Domain.Repositories.Cached;

namespace EM.Application.Features.Employees.Services
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly ICachedEmployeeRepository _cachedEmployeeRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(ICachedEmployeeRepository cachedEmployeeRepository,
                                 IEmployeeRepository employeeRepository)
        {
            _cachedEmployeeRepository = cachedEmployeeRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee?> GetByIdAsync(int id, ISpecification<Employee> specification, CancellationToken cancellationToken = default)
        {
            Employee? employee = await _cachedEmployeeRepository.GetAsync(id);

            if (employee is null)
            {
                employee = await _employeeRepository.GetAsync(specification, cancellationToken);

                if (employee is not null)
                {
                    await _cachedEmployeeRepository.SetAsync(employee);
                }
            }

            return employee;
        }

        public async Task<IReadOnlyList<Employee>> GetListAsync(ISpecification<Employee>? specification = default, CancellationToken cancellationToken = default)
        {
            List<Employee> employees = await _employeeRepository.GetListAsync(specification, cancellationToken);

            if (employees.Count > 0)
            {
                await AddEmployeesToCache(employees);
            }

            return employees;
        }

        public async Task<IPaginate<Employee>> GetListAsync(ISpecification<Employee> specification, PageRequest pageRequest, CancellationToken cancellationToken = default)
        {
            IPaginate<Employee> paginatedResult = await _employeeRepository.GetListAsync(specification: specification,
                                                                                         pageRequest: pageRequest,
                                                                                         cancellationToken: cancellationToken);

            if (paginatedResult.Count > 0)
            {
                await AddEmployeesToCache(paginatedResult.Items);
            }

            return paginatedResult;
        }

        public async Task<Employee> AddAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            Employee createdEmployee = await _employeeRepository.AddAsync(employee, cancellationToken);
            await _cachedEmployeeRepository.SetAsync(createdEmployee);

            return createdEmployee;
        }

        public async Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            Employee updatedEmployee = await _employeeRepository.UpdateAsync(employee, cancellationToken);
            await _cachedEmployeeRepository.SetAsync(updatedEmployee);

            return updatedEmployee;
        }

        public async Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            await _employeeRepository.DeleteAsync(employee, cancellationToken);
            await _cachedEmployeeRepository.DeleteAsync(employee);
        }

        private async Task AddEmployeesToCache(IReadOnlyList<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                await _cachedEmployeeRepository.SetAsync(employee);
            }
        }
    }
}
