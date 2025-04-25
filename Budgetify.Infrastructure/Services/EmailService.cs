using budgetify.Application.Services;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using budgetify.Application.Dtos;

namespace Budgetify.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;        
        public EmailService(IConfiguration configuration) 
        {
            _config = configuration;    
        }
        public async Task SendEmailAsync(EmailJob emailJob)
        {
            // get configuration data 
            var emailFrom = _config["emailFrom"];
            var emailServer = _config["emailServer"];
            var emailPort = int.Parse(_config["emailPort"]);
            var appKey = _config["emailAppKey"];

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailFrom));
            email.To.Add(MailboxAddress.Parse(emailJob.To));
            email.Subject = emailJob.Subject;
            email.Body = new TextPart("html")
            {
                Text = $"<p>Your verifIcation code is : <strong>{emailJob.VerificationCode}</strong></p>"
            };

            // sending email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(emailServer, emailPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(emailFrom, appKey);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
