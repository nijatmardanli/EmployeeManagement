using System.Linq.Expressions;

namespace EM.Domain.Common.Specification
{
    public record Specification<TEntity>(Expression<Func<TEntity, bool>>? Predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy = null,
                                         string[]? Includes = null,
                                         bool EnableTracking = true) : ISpecification<TEntity>
        where TEntity : AuditableEntity, new();
}
