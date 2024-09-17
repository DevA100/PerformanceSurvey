using PerformanceSurvey.Models;

namespace PerformanceSurvey.iRepository
{
    public interface IQuestionRepository
    {
        Task<Question> GetDepartmentQuestionAsync(int id);
        Task<IEnumerable<Question>> GetAllDepartmentQuestionsAsync();
        Task AddDepartmentMultiplechoiceQuestionAsync(Question question);
        Task<QuestionOption> GetOptionByIdAsync(int optionId);
        Task<IEnumerable<QuestionOption>> GetAllOptionsAsync();

        Task AddDepartmentTextQuestionAsync(Question question);
        Task UpdateDepartmentMultipleChoiceQuestionAsync(Question question);
        Task UpdateDepartmentTextQuestionAsync(Question question);
        Task DeleteDepartmentQuestionAsync(int id);
        Task<IEnumerable<Question>> GetDepartmentQuestionsByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<Question>> GetDepartmentQuestionsByDepartmentIdsAsync(IEnumerable<int> departmentId);
    }
}
