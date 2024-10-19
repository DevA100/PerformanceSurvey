using PerformanceSurvey.Models;
using PerformanceSurvey.Models.DTOs;

namespace PerformanceSurvey.iServices
{
    public interface IAssignmentQuestionService
    {
        Task AssignQuestionsToMultipleUsersAsync(AssignmentQuestionMultipleDto assignmentQuestionMultipleDto);
        Task AssignQuestionsToDepartmentAsync(AssignQuestionsToDepartmentDto dto);
        Task AssignDiffQuestionsToDepartmentAsync(AssignDiffQuestionsToDepartmentDto dto);
        Task AssignDiffQuestionsToDiffDepartmentAsync(AssignDiffQuestionsToDiffDepartmentDto dto);
        Task AssignQuestionsToSingleUsersAsync(AssignmentQuestionSingleUserDto assignmentQuestionSingleUserDto);
        Task<IEnumerable<GetQuestionByDepartmentDto>> GetAssignmentQuestionsByUserIdAsync(int userId);

        Task<IEnumerable<GetQuestionByDepartmentDto>> GetAssignmentQuestionsByUserIdsAsync(IEnumerable<int> userIds);
        Task DeleteAnsweredAssignmentQuestionsByUserIdAsync(int userId);
    }
}
