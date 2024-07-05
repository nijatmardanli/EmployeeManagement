using EM.Domain.Entities;
using EM.Domain.Repositories.Cached;
using EM.Infrastructure.Options;
using StackExchange.Redis;

namespace EM.Persistence.Repositories.Cached
{
    public class CachedEmployeeRepository : CachedRepositoryBase<Employee>, ICachedEmployeeRepository
    {
        protected override string KeyName => "Employee";

        public CachedEmployeeRepository(ConnectionMultiplexer cluster, CacheSettings cacheSettings) : base(cluster, cacheSettings)
        {
        }
    }
}
