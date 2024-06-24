using EM.Domain.Entities;
using EM.Domain.Repositories.Cached;
using EM.Infrastructure.Options;
using StackExchange.Redis;

namespace EM.Persistence.Repositories.Cached
{
    public class CachedDepartmentRepository : CachedRepositoryBase<Department>, ICachedDepartmentRepository
    {
        protected override string KeyName => "Department";

        public CachedDepartmentRepository(ConnectionMultiplexer cluster, CacheSettings cacheSettings) : base(cluster, cacheSettings)
        {
        }

        protected override string GetKey(object id)
        {
            return $"{KeyName}:{id}";
        }
    }
}
