using EM.Domain.Entities;
using EM.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace EM.Persistence.Contexts
{
    public class EmployeeDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public EmployeeDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new AuditingSaveChangesInterceptor());

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var h = ChangeTracker.Entries<BaseEntity>()
        //        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

        //    return base.SaveChangesAsync(cancellationToken);
        //}
    }
}