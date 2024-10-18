namespace PerformanceSurvey.Models.DTOs
{
    public class AssignQuestionsToDepartmentDto
    {
        // ID of the department from which question IDs will be sourced
        public int SourceDepartmentId { get; set; }

        // ID of the department to which questions will be assigned
        public int TargetDepartmentId { get; set; }

        // List of question IDs to be assigned
    }
}
