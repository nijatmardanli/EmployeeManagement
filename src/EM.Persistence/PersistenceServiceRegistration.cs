using EM.Domain.Repositories;
using EM.Persistence.Contexts;
using EM.Persistence.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EM.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDepartmentRepository, EfDepartmentRepository>();
            
            services.AddScoped<IEmployeeRepository, EfEmployeeRepository>();

            services.AddScoped<EmployeeDbContextInitializer>();

            services.AddDbContext<EmployeeDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EmployeeDb")));

            return services;
        }
    }
}
