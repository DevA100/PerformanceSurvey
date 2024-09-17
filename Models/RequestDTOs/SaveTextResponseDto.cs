namespace PerformanceSurvey.Models.DTOs
{
    public class SaveTextResponseDto
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string ResponseText { get; set; }
        public int DepartmentId { get; set; }
    }
}
