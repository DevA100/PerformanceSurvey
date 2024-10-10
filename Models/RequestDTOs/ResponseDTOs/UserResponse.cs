using System.ComponentModel.DataAnnotations;

namespace PerformanceSurvey.Models.DTOs.ResponseDTOs
{
    public class UserResponse
    {
       public int UserId { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string UserEmail { get; set; }
    }
}
