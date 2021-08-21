using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Mvcday1.Models;

namespace Mvcday1
{
    public interface IEmailService
    {
        Task SendMessage(string recipentName, string recipientEmail, string subject, string body);
    }
    public class EmailService : IEmailService
    {
        private readonly EmailSettingModel _setting;
        public EmailService(IConfiguration configuration)
        {
            _setting = new EmailSettingModel();
            configuration.GetSection("Email").Bind(_setting);
        }

        public async Task SendMessage(string recipentName, string recipientEmail, string subject, string body)
        {
            var message = new MimeMessage();
            var bodyBuilder = new BodyBuilder();
            message.From.Add(new MailboxAddress(_setting.User, _setting.User));
            message.To.Add(new MailboxAddress(recipentName, recipientEmail));
            message.Subject = subject;
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();
            var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await client.ConnectAsync(_setting.Host, _setting.Port);
            await client.AuthenticateAsync(_setting.User, _setting.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

    }
}