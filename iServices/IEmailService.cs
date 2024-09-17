namespace PerformanceSurvey.iServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
