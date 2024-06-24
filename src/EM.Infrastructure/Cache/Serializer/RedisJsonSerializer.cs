using NReJSON;
using StackExchange.Redis;
using System.Text.Json;

namespace EM.Infrastructure.Cache.Serializer
{
    public sealed class RedisJsonSerializer : ISerializerProxy
    {
        public TResult? Deserialize<TResult>(RedisResult serializedValue) =>
            JsonSerializer.Deserialize<TResult>(serializedValue.ToString()!);

        public string Serialize<TObjectType>(TObjectType obj) =>
            JsonSerializer.Serialize(obj);
    }
}
