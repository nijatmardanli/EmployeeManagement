using EM.Domain.Common;
using EM.Domain.Common.Paging;
using EM.Domain.Common.Specification;
using System.Linq.Expressions;

namespace EM.Domain.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : AuditableEntity, new()
    {
        Task<TEntity?> GetAsync(ISpecification<TEntity>? specification = null,
                                CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListAsync(ISpecification<TEntity>? specification = null,
                                         CancellationToken cancellationToken = default);

        Task<IPaginate<TEntity>> GetListAsync(ISpecification<TEntity>? specification = null,
                                              PageRequest? pageRequest = null,
                                              CancellationToken cancellationToken = default);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
    }
}
