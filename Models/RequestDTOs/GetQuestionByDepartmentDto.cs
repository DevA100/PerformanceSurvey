using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models.DTOs
{
    public class GetQuestionByDepartmentDto
    {
        [Key]
        public int QuestionId { get; set; }
        public string DepartmentName { get; set; }

        public string QuestionText { get; set; }
        public int DepartmentId { get; set; }

        public List<QuestionOptionDto> Options { get; set; }
    }
}
