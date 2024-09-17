using Microsoft.AspNetCore.Mvc;
using PerformanceSurvey.iServices;

namespace PerformanceSurvey.Controllers
{
    [Route("api/Email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            if (string.IsNullOrEmpty(request.ToEmail) || string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
            {
                return BadRequest("Email, subject, and body are required.");
            }

            try
            {
                await _emailService.SendEmailAsync(request.ToEmail, request.Subject, request.Body);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
