using MailKit.Net.Smtp;
using MimeKit;

namespace Eatery_API.Services
{
    public class ElasticEmailService
    {
        private readonly IConfiguration _config;

        public ElasticEmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string to, string subject, string name, string templatePath)
        {
            var htmlBody = await File.ReadAllTextAsync(templatePath);

            var code = Random.Shared.Next(100000, 999999).ToString();

            htmlBody = htmlBody.Replace("{{Name}}", name)
                       .Replace("{{Code}}", code);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["ElasticEmail:FromEmail"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = htmlBody };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["ElasticEmail:Server"], int.Parse(_config["ElasticEmail:Port"] ?? "2525"), MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["ElasticEmail:UserName"], _config["ElasticEmail:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
