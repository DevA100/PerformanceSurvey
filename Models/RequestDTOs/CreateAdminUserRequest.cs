namespace PerformanceSurvey.Models.DTOs
{
    public class CreateAdminUserRequest
    {
    public string Name { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; } // Admin provides a password manually
    public string Key { get; set; }
    }
}
