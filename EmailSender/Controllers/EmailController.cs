using EmailSender.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailController:ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        public async Task <IActionResult> SendEmail(string name, string receptor, string subject, string body)
        {
            await emailService.SendEmail(name, receptor, subject, body);
     
            return Ok();
        }
    }
}
