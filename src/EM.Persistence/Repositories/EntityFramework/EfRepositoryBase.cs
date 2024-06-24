using EM.Domain.Common;
using EM.Domain.Common.Paging;
using EM.Domain.Common.Specification;
using EM.Domain.Repositories;
using EM.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EM.Persistence.Repositories.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : AuditableEntity, new()
        where TContext : DbContext
    {
        private readonly TContext _context;

        public EfRepositoryBase(TContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetAsync(ISpecification<TEntity>? specification = null,
                                             CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();

            if (specification is null)
                return await queryable.FirstOrDefaultAsync(cancellationToken);

            if (!specification.EnableTracking)
                queryable = queryable.AsNoTracking();

            if (specification.Includes != null)
            {
                queryable = AddIncludes(specification.Includes, queryable);
            }

            if (specification.Predicate != null)
                return await queryable.FirstOrDefaultAsync(specification.Predicate, cancellationToken);

            return await queryable.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<TEntity>> GetListAsync(ISpecification<TEntity>? specification = null,
                                                      CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetList(specification);

            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<IPaginate<TEntity>> GetListAsync(ISpecification<TEntity>? specification = null,
                                                           PageRequest? pageRequest = null,
                                                           CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetList(specification);

            return await queryable.ToPaginateAsync(pageRequest, cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entities;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();

            if (predicate != null)
                return await queryable.AnyAsync(predicate, cancellationToken);

            return await queryable.AnyAsync(cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();

            if (predicate != null)
                return await queryable.CountAsync(predicate, cancellationToken);

            return await queryable.CountAsync(cancellationToken);
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();

            if (predicate != null)
                return await queryable.LongCountAsync(predicate, cancellationToken);

            return await queryable.LongCountAsync(cancellationToken);
        }

        private IQueryable<TEntity> GetList(ISpecification<TEntity>? specification = null)
        {
            IQueryable<TEntity> queryable = Query();

            if (specification is null)
                return queryable;

            if (!specification.EnableTracking)
                queryable = queryable.AsNoTracking();

            if (specification.Includes != null)
            {
                queryable = AddIncludes(specification.Includes, queryable);
            }

            if (specification.Predicate != null)
                queryable = queryable.Where(specification.Predicate);

            if (specification.OrderBy != null)
                queryable = specification.OrderBy(queryable);

            return queryable;
        }

        private IQueryable<TEntity> AddIncludes(string[] includes, IQueryable<TEntity> queryable)
        {
            foreach (var inclue in includes)
            {
                queryable = queryable.Include(inclue);
            }

            return queryable;
        }
    }
}
