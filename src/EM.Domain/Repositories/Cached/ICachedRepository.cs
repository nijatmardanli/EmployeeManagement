using EM.Domain.Common;
using EM.Domain.Common.Paging;

namespace EM.Domain.Repositories.Cached
{
    public interface ICachedRepository<TEntity>
        where TEntity : AuditableEntity, new()
    {
        Task<int> CountAsync();
        Task DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<IPaginate<TEntity>> GetAllAsync(PageRequest pageRequest);
        Task<TEntity?> GetAsync(object id);
        Task<TEntity> SetAsync(TEntity entity);
    }
}
