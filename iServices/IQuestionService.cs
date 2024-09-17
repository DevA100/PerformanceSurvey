using PerformanceSurvey.Models.DTOs;
using PerformanceSurvey.Models;
using PerformanceSurvey.Models.RequestDTOs;
using PerformanceSurvey.Models.RequestDTOs.ResponseDTOs;

namespace PerformanceSurvey.iServices
{
    public interface IQuestionService
    {
        Task<MultipleChoiceQuestionResponse> CreateDepartmentMultipleQuestionAsync(MultipleChoiceQuestionRequest questionDto);
        Task<QuestionOptionDto> GetOptionByIdAsync(int optionId);
        Task<IEnumerable<QuestionOptionDto>> GetAllOptionsAsync();
        Task<TextQuestionResponse> CreateDepartmentTextQuestionAsync(TextQuestionReqest questionDto);
        Task<QuestionDto> GetDepartmentQuestionAsync(int id);
        Task<IEnumerable<QuestionDto>> GetAllDepartmentQuestionsAsync();
        Task<MultipleChoiceQuestionResponse> UpdateDepartmentMultipleChoiceQuestionAsync(int id, MultipleChoiceQuestionRequest questionDto);
        Task<TextQuestionResponse> UpdateDepartmentTextQuestionAsync(int id, TextQuestionReqest questionDto);
        Task<Question> DeleteDepartmentQuestionAsync(int id);
        Task<IEnumerable<GetQuestionByDepartmentDto>> GetDepartmentQuestionsByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<GetQuestionByDepartmentDto>> GetDepartmentQuestionsByDepartmentIdsAsync(IEnumerable<int> departmentId);
    }
}
