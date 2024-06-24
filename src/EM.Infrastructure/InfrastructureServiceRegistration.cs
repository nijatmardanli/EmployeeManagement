using EM.Infrastructure.Cache.Serializer;
using EM.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NReJSON;
using Serilog;

namespace EM.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp => configuration.GetSection("CacheSettings").Get<CacheSettings>()!);

            NReJSONSerializer.SerializerProxy = new RedisJsonSerializer();

            return services;
        }

        public static WebApplicationBuilder UseInfrastructureWebApplicationBuilder(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Logging.AddSerilog();

            return builder;
        }
    }
}