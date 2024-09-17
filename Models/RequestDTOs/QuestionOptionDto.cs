using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models.DTOs
{
    public class QuestionOptionDto
    {

        [Key]
        public int OptionId { get; set; }
        public string? Text { get; set; }
        public int? Score { get; set; }
    }
}
