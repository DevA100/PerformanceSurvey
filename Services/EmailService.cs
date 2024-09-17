using PerformanceSurvey.iServices;
using System.Net.Mail;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace PerformanceSurvey.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly string _baseAddress;

        public EmailService()
        {
            _apiKey = "wSsVR60g+halB6d4njb+L+Y6nFhUDlL+HR8u2lL1vietGPvC98c4xk2YBA7zFKdNR2dsEjYUprt/mhwF22VajtgoyFBUCCiF9mqRe1U4J3x17qnvhDzCX25ZmhOJLYMJxQhummRhE8km+g=="; // Replace with your actual API key
            _baseAddress = "https://api.zeptomail.com/v1.1/email";
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var http = (HttpWebRequest)WebRequest.Create(new Uri(_baseAddress));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";
                http.PreAuthenticate = true;
                http.Headers.Add("Authorization", $"Zoho-enczapikey {_apiKey}");

                var emailContent = new
                {
                    from = new
                    {
                        address = "e-info@jubileelifeng.com",
                        name = "Performance Management Survey"
                    },
                    to = new[]
                    {
                new { email_address = new { address = toEmail, name = toEmail } }
            },
                    subject = subject,
                    htmlbody = body
                };

                var json = JObject.FromObject(emailContent).ToString();
                var encoding = new UTF8Encoding();
                var bytes = encoding.GetBytes(json);

                using (var requestStream = await http.GetRequestStreamAsync())
                {
                    await requestStream.WriteAsync(bytes, 0, bytes.Length);
                }

                using (var response = await http.GetResponseAsync())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var sr = new StreamReader(responseStream))
                        {
                            var responseContent = await sr.ReadToEndAsync();
                            // Optionally handle the response content as needed
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle exceptions such as failed requests or timeouts
                throw new Exception("Failed to send email", ex);
            }
        }
    }
    }

