using static PerformanceSurvey.Models.User;
using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models.DTOs
{
    public class UserRequest
    {
        
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string UserEmail { get; set; }
        public int? DepartmentId { get; set; }

    }
}
