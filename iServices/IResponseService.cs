using PerformanceSurvey.Models;
using PerformanceSurvey.Models.DTOs;

namespace PerformanceSurvey.iServices
{
    public interface IResponseService
    {
        Task SaveResponseAsync(SaveMultipleChoiceResponseDto dto);
        Task<SaveTextResponseDto> SaveTextResponseAsync(SaveTextResponseDto textResponseDto);
        Task<List<GetResponsesByDepartmentIdDto>> GetResponsesByDepartmentIdAsync(int DepartmentId);
        Task ClearResponsesByDepartmentIdAsync(int departmentId);
        Task ClearAllResponsesAsync();
        Task<byte[]> ExportResponsesToExcelAsync(int? departmentId = null);
    }
}
