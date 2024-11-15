using System.Net;
using System.Net.Mail;

namespace EmailSender.Services
{
    public interface IEmailService
    {
        Task SendEmail(string name, string receptor, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(string name, string receptor, string subject, string body)
        {
            var email = configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
            var password = configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
            var host = configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
            var port = configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");

            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(email, password);
            
            var fullBody = "from: " + name + " (" + receptor + ")" + "\n\n" + body;

            var message = new MailMessage(receptor, email!, subject, fullBody);

            await smtpClient.SendMailAsync(message);

        }
    }
}
