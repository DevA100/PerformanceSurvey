using PerformanceSurvey.Models.DTOs;

namespace PerformanceSurvey.Models.RequestDTOs
{
    public class MultipleChoiceQuestionRequest
    {
        public string QuestionText { get; set; }
        public int DepartmentId { get; set; }

        public List<QuestionOptionDto> Options { get; set; }
    }
}
