using PerformanceSurvey.iRepository;
using PerformanceSurvey.iServices;
using PerformanceSurvey.Models.DTOs;
using PerformanceSurvey.Models;

namespace PerformanceSurvey.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(IDepartmentRepository repository, ILogger<DepartmentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            _logger.LogInformation("Creating a new Department with Name {DepartmentName}", departmentDto.DepartmentName);

            // Map DepartmentDto to Department
            var department = new Department
            {
                DepartmentName = departmentDto.DepartmentName,
                CreatedAt = DateTime.UtcNow // Set CreatedAt here if needed
            };

            // Call repository to create department
            var createdDepartment = await _repository.CreateDepartmentAsync(department);

            // Map the created Department back to a DepartmentDto
            var resultDto = new DepartmentDto
            {
                DepartmentName = createdDepartment.DepartmentName,
                // Map other properties as needed
            };

            return resultDto;
        }


        public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
        {
            var department = await _repository.GetDepartmentByIdAsync(id);
            if (department == null || department.IsDisabled)
                return null;

            return new DepartmentDto
            {
                DepartmentName = department.DepartmentName
            };
        }


        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _repository.GetAllDepartmentsAsync();
            return departments.Select(d => new DepartmentDto
            {
                DepartmentName = d.DepartmentName
            });
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(int id, DepartmentDto departmentDto)
        {
            // Fetch the existing department entity
            var department = await _repository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return null; // Return null if the department doesn't exist
            }

            // Update the department entity with new values
            department.DepartmentName = departmentDto.DepartmentName;
            department.UpdatedAt = DateTime.UtcNow;

            // Update the department in the repository
            var updatedDepartment = await _repository.UpdateDepartmentAsync(department);

            // Map the updated department entity back to DTO
            var updatedDepartmentDto = new DepartmentDto
            {
                DepartmentName = updatedDepartment.DepartmentName,
                // Map other properties if necessary
            };

            return updatedDepartmentDto; // Return the mapped DTO
        }

        public async Task<bool> DisableDepartmentAsync(int id)
        {
            await _repository.DisableDepartmentAsync(id);
            return true;
        }
    }

}
