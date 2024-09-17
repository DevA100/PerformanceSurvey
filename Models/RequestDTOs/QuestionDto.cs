using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models.DTOs
{
    public class QuestionDto
    {
     
        public string QuestionText { get; set; }

        public List<QuestionOptionDto> Options { get; set; }
    }
}
