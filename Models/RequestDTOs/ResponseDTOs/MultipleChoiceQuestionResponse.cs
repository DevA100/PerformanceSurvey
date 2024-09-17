using PerformanceSurvey.Models.DTOs;

namespace PerformanceSurvey.Models.RequestDTOs.ResponseDTOs
{
    public class MultipleChoiceQuestionResponse
    {
        public string QuestionText { get; set; }

        public List<QuestionOptionDto> Options { get; set; }
    }
}
