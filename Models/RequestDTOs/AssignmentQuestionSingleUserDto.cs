namespace PerformanceSurvey.Models.DTOs
{
    public class AssignmentQuestionSingleUserDto
    {
        public int DepartmentId { get; set; }
        public List<int> QuestionId { get; set; }
        public int UserId { get; set; }
    }
}
