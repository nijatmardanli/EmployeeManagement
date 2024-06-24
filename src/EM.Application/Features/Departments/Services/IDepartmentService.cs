using EM.Domain.Common.Paging;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using System.Linq.Expressions;

namespace EM.Application.Features.Departments.Services
{
    public interface IDepartmentService
    {
        Task<Department?> GetByIdAsync(int id, ISpecification<Department> specification, CancellationToken cancellationToken = default);
        Task<IPaginate<Department>> GetListAsync(ISpecification<Department> specification, PageRequest pageRequest, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Department>> GetListAsync(ISpecification<Department>? specification = null, CancellationToken cancellationToken = default);
        Task<Department> AddAsync(Department department, CancellationToken cancellationToken = default);
        Task<Department> UpdateAsync(Department department, CancellationToken cancellationToken = default);
        Task DeleteAsync(Department department, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<Department, bool>>? predicate = null, CancellationToken cancellationToken = default);
    }
}
