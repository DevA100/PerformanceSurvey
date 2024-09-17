using PerformanceSurvey.Models;

namespace PerformanceSurvey.iRepository
{
    public interface IResponseRepository
    {
        Task SaveAsync(Response response);
        Task<List<Response>> GetResponsesByDepartmentIdAsync(int DepartmentId);
        Task<Response> AddAsync(Response textResponse);
        Task ClearResponsesByDepartmentIdAsync(int departmentId);
        Task ClearAllResponsesAsync();
        Task<List<Response>> GetAllResponsesAsync();

    }
}
