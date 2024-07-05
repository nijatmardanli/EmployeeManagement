using EM.Domain.Common;
using EM.Domain.Common.Paging;
using EM.Domain.Repositories.Cached;
using EM.Infrastructure.Options;
using EM.Persistence.Extensions;
using NReJSON;
using StackExchange.Redis;
using System.Text.Json;

namespace EM.Persistence.Repositories.Cached
{
    public abstract class CachedRepositoryBase<TEntity> : ICachedRepository<TEntity>
        where TEntity : AuditableEntity, new()
    {
        protected readonly ConnectionMultiplexer _cluster;
        private readonly CacheSettings _cacheSettings;

        protected IDatabase Database => _cluster.GetDatabase();

        public CachedRepositoryBase(ConnectionMultiplexer cluster, CacheSettings cacheSettings)
        {
            _cluster = cluster;
            _cacheSettings = cacheSettings;
        }

        public async Task<TEntity?> GetAsync(object id)
        {
            string key = GetKey(id);

            RedisResult result = await Database.JsonGetAsync(key);
            if (result.IsNull)
                return null;

            return JsonSerializer.Deserialize<TEntity>(result.ToString()!);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var keys = await GetKeysAsync($"{KeyName}:*");

            if (keys.Length == 0)
                return new List<TEntity>();

            var result = await Database.JsonMultiGetAsync<TEntity>(keys);

            return result.ToList();
        }

        public async Task<IPaginate<TEntity>> GetAllAsync(PageRequest pageRequest)
        {
            var keys = (await GetKeysAsync($"{KeyName}:*")).Skip(pageRequest.Page * pageRequest.Size)
                                                                    .Take(pageRequest.Size)
                                                                    .ToArray();

            if (keys.Length == 0)
                return new Paginate<TEntity>();

            var result = await Database.JsonMultiGetAsync<TEntity>(keys);

            return result.ToPaginate(pageRequest);
        }

        public async Task<TEntity> SetAsync(TEntity entity)
        {
            string json = JsonSerializer.Serialize(entity);

            string key = GetKey(entity.Id);
            _ = await Database.JsonSetAsync(key, json);
            _ = await Database.KeyExpireAsync(key, TimeSpan.FromSeconds(_cacheSettings.SlidingExpiration));

            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            string key = GetKey(entity.Id);
            _ = await Database.JsonDeleteAsync(key);
        }

        public async Task<int> CountAsync()
        {
            RedisKey[] keys = await GetKeysAsync($"{KeyName}:*");

            return keys.Length;
        }


        protected abstract string KeyName { get; }

        protected virtual string GetKey(object id)
        {
            return $"{KeyName}:{id}";
        }

        protected async Task<RedisKey[]> GetKeysAsync(string keyPattern)
        {
            RedisResult keyResult = await Database.ExecuteAsync("KEYS", keyPattern);

            if (keyResult.IsNull)
                return Array.Empty<RedisKey>();

            return (RedisKey[])keyResult;
        }
    }
}
