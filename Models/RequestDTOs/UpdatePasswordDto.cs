namespace PerformanceSurvey.Models.DTOs
{
    public class UpdatePasswordDto
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
