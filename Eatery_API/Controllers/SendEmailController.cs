using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eatery_API.Controllers
{
    [Route("mail")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly ElasticEmailService _emailService;

        public SendEmailController(ElasticEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-verification-email")]
        public async Task<IActionResult> SendVerificationEmail([FromBody] DTOMail mail)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "Templates", "TemplateVerify.html");

            await _emailService.SendEmailAsync(mail.To, "Email Verification", mail.Name, templatePath);
            return Ok(new { statusCode = 200, message = "Verification email sent successfully." });
        }
    }
}
