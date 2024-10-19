namespace PerformanceSurvey.Models.DTOs
{
    public class SaveMultipleChoiceResponseDto
    {
        public int QuestionId { get; set; }
        public int DepartmentId { get; set; }
        public int UserId { get; set; }
        public int OptionId { get; set; } // ID of the selected option, if it's a multiple-choice question

    }
}
