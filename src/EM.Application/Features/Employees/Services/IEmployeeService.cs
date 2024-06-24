using EM.Domain.Common.Paging;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;

namespace EM.Application.Features.Employees.Services
{
    public interface IEmployeeService
    {
        Task<Employee?> GetByIdAsync(int id, ISpecification<Employee> specification, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Employee>> GetListAsync(ISpecification<Employee>? specification = null, CancellationToken cancellationToken = default);
        Task<IPaginate<Employee>> GetListAsync(ISpecification<Employee> specification, PageRequest pageRequest, CancellationToken cancellationToken = default);
        Task<Employee> AddAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken = default);
        Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default);
    }
}
