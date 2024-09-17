using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models.DTOs
{
    public class DepartmentDto
    {
        
        [Required]
        [MaxLength(250)]
        public string DepartmentName { get; set; }
    }
}
