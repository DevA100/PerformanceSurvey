using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Context;
using PerformanceSurvey.iRepository;
using PerformanceSurvey.Models;

namespace PerformanceSurvey.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            department.CreatedAt = DateTime.UtcNow;
            _context.departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _context.departments.FirstOrDefaultAsync(d => d.DepartmentId == id);
        }



        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.departments.Where(d => !d.IsDisabled).ToListAsync();
        }

        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            department.UpdatedAt = DateTime.UtcNow;
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task DisableDepartmentAsync(int id)
        {
            var department = await GetDepartmentByIdAsync(id);
            if (department != null)
            {
                department.IsDisabled = true;
                await UpdateDepartmentAsync(department);
            }
        }


        public async Task<List<Department>> GetDepartmentsByIdsAsync(List<int> departmentIds)
        {
            return await _context.departments
                .Where(d => departmentIds.Contains(d.DepartmentId))
                .ToListAsync();
        }

    }

}
