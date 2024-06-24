using EM.Domain.Repositories;
using EM.Domain.Repositories.Cached;
using EM.Persistence.Contexts;
using EM.Persistence.Repositories.Cached;
using EM.Persistence.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace EM.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDepartmentRepository, EfDepartmentRepository>();
            services.AddScoped<ICachedDepartmentRepository, CachedDepartmentRepository>();

            services.AddScoped<IEmployeeRepository, EfEmployeeRepository>();
            services.AddScoped<ICachedEmployeeRepository, CachedEmployeeRepository>();

            services.AddScoped<EmployeeDbContextInitializer>();

            services.AddDbContext<EmployeeDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EmployeeDb")));

            
            ConnectionMultiplexer cluster = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { configuration.GetConnectionString("Redis") }
            });

            services.AddSingleton(cluster);

            return services;
        }
    }
}
