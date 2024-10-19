namespace PerformanceSurvey.Models.DTOs
{
    public class AuthenticateAdminDto
    {
        public string Token { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiredAt { get; set; }
        public string Role { get; set; }
    }
}
