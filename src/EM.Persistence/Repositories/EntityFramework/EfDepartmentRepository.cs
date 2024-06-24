using EM.Domain.Entities;
using EM.Domain.Repositories;
using EM.Persistence.Contexts;

namespace EM.Persistence.Repositories.EntityFramework
{
    public class EfDepartmentRepository : EfRepositoryBase<Department, EmployeeDbContext>, IDepartmentRepository
    {
        public EfDepartmentRepository(EmployeeDbContext context) : base(context)
        {
        }
    }
}
