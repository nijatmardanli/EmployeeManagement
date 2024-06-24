using EM.Domain.Entities;
using EM.Domain.Repositories;
using EM.Persistence.Contexts;

namespace EM.Persistence.Repositories.EntityFramework
{
    public class EfEmployeeRepository : EfRepositoryBase<Employee, EmployeeDbContext>, IEmployeeRepository
    {
        public EfEmployeeRepository(EmployeeDbContext context) : base(context)
        {
        }
    }
}
