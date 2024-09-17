namespace PerformanceSurvey.Models.DTOs
{
    public class AssignDiffQuestionsToDepartmentDto
    {
        // ID of the department from which question IDs will be sourced
        public List<int> SourceDepartmentId { get; set; }

        // ID of the department to which questions will be assigned
        public int TargetDepartmentId { get; set; }

        // List of question IDs to be assigned
        public List<int> QuestionId { get; set; }
    }
}
