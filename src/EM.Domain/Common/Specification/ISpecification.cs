using System.Linq.Expressions;

namespace EM.Domain.Common.Specification
{
    public interface ISpecification<TEntity> where TEntity : AuditableEntity, new()
    {
        Expression<Func<TEntity, bool>>? Predicate { get; init; }
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy { get; init; }
        string[]? Includes { get; init; }
        bool EnableTracking { get; init; }
    }
}
