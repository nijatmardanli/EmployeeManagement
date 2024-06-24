using EM.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EM.Persistence.Contexts
{
    public static class InitialiserExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app, bool retry = true)
        {
            using var scope = app.Services.CreateScope();

            try
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<EmployeeDbContextInitializer>();

                await initialiser.InitialiseAsync();

                await initialiser.SeedAsync();
            }
            catch (Exception)
            {
                if (!retry)
                {
                    throw;
                }

                ILogger logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                logger.LogWarning("Starting a 10-second delay before retrying database initialization.");

                await Task.Delay(10_000);
                await InitializeDatabaseAsync(app, false);
            }
        }
    }

    public class EmployeeDbContextInitializer
    {
        private readonly ILogger<EmployeeDbContextInitializer> _logger;
        private readonly EmployeeDbContext _context;

        public EmployeeDbContextInitializer(ILogger<EmployeeDbContextInitializer> logger,
                                            EmployeeDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            Department[] departments = new Department[] {
                new Department()
                {
                    Name = "IT"
                },
                new Department()
                {
                    Name = "Sales"
                }
            };

            Employee[] employees = new Employee[] {
                new Employee()
                {
                    Name = "Nijat",
                    LastName = "Mardanli",
                    Department = departments[0],
                    BirthDate = new(1997, 9,6)
                },
                new Employee()
                {
                    Name = "Sales Staff Name",
                    LastName = "Sales Staff Last Name",
                    Department = departments[1],
                    BirthDate = new(1998,3,6)
                }
            };

            await _context.Departments.AddRangeAsync(departments);

            await _context.Employees.AddRangeAsync(employees);

            await _context.SaveChangesAsync();
        }
    }
}
