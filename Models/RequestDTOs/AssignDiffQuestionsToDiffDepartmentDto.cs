﻿namespace PerformanceSurvey.Models.DTOs
{
    public class AssignDiffQuestionsToDiffDepartmentDto
    {
        // ID of the department from which question IDs will be sourced
        public List<int> SourceDepartmentId { get; set; }

        // ID of the department to which questions will be assigned
        public List<int> TargetDepartmentId { get; set; }

    }
}
