using Microsoft.AspNetCore.Builder;
using Serilog;

namespace EM.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
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