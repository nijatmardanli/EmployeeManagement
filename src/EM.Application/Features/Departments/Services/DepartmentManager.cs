using EM.Domain.Common.Paging;
using EM.Domain.Common.Specification;
using EM.Domain.Entities;
using EM.Domain.Repositories;
using EM.Domain.Repositories.Cached;

namespace EM.Application.Features.Departments.Services
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly ICachedDepartmentRepository _cachedDepartmentRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentManager(ICachedDepartmentRepository cachedDepartmentRepository,
                                 IDepartmentRepository departmentRepository)
        {
            _cachedDepartmentRepository = cachedDepartmentRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<Department?> GetByIdAsync(int id, ISpecification<Department> specification, CancellationToken cancellationToken = default)
        {
            Department? department = await _cachedDepartmentRepository.GetAsync(id);

            if (department is null)
            {
                department = await _departmentRepository.GetAsync(specification, cancellationToken);

                if (department is not null)
                {
                    await _cachedDepartmentRepository.SetAsync(department);
                }
            }

            return department;
        }

        public async Task<IReadOnlyList<Department>> GetListAsync(ISpecification<Department>? specification = default, CancellationToken cancellationToken = default)
        {
            List<Department> departments = await _cachedDepartmentRepository.GetAllAsync();

            if (departments.Count == 0)
            {
                departments = await _departmentRepository.GetListAsync(specification, cancellationToken);

                if (departments.Count > 0)
                {
                    await AddDepartmentsToCache(departments);
                }
            }
            else
            {
                departments = departments.OrderBy(x => x.Id).ToList();
            }

            return departments;
        }

        public async Task<IPaginate<Department>> GetListAsync(ISpecification<Department> specification, PageRequest pageRequest, CancellationToken cancellationToken = default)
        {
            IPaginate<Department> paginatedResult = await _departmentRepository.GetListAsync(specification: specification,
                                                                                             pageRequest: pageRequest,
                                                                                             cancellationToken: cancellationToken);

            if (paginatedResult.Count > 0)
            {
                await AddDepartmentsToCache(paginatedResult.Items);
            }

            return paginatedResult;
        }

        public async Task<Department> AddAsync(Department department, CancellationToken cancellationToken = default)
        {
            Department createdDepartment = await _departmentRepository.AddAsync(department, cancellationToken);
            await _cachedDepartmentRepository.SetAsync(createdDepartment);

            return createdDepartment;
        }

        public async Task<Department> UpdateAsync(Department department, CancellationToken cancellationToken = default)
        {
            Department updatedDepartment = await _departmentRepository.UpdateAsync(department, cancellationToken);
            await _cachedDepartmentRepository.SetAsync(updatedDepartment);

            return updatedDepartment;
        }

        public async Task DeleteAsync(Department department, CancellationToken cancellationToken = default)
        {
            await _departmentRepository.DeleteAsync(department, cancellationToken);
            await _cachedDepartmentRepository.DeleteAsync(department);
        }

        private async Task AddDepartmentsToCache(IReadOnlyList<Department> departments)
        {
            foreach (Department department in departments)
            {
                await _cachedDepartmentRepository.SetAsync(department);
            }
        }
    }
}
