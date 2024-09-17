using PerformanceSurvey.Models;

namespace PerformanceSurvey.iRepository
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateDepartmentAsync(Department department);
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> UpdateDepartmentAsync(Department department);
        Task DisableDepartmentAsync(int id);
    }
}
