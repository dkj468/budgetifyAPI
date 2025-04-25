using budgetify.Application.Dtos;
using budgetify.Application.Services;
using budgetify.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Budgetify.Infrastructure.Services
{
    internal class EmailBackgroundService : IEmailBackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public EmailBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void SendEmail(EmailJob emailJob)
        {
            var scope = _serviceProvider.CreateScope();
            var emailService = scope.ServiceProvider.GetService<IEmailService>();
            var logger = scope.ServiceProvider.GetService<ILogger<EmailBackgroundService>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            try
            {
                emailService?.SendEmailAsync(emailJob);
                // update the database with email code
                var user = dbContext.Users.FirstOrDefault(x => x.Email == emailJob.To);
                if (user != null) 
                {
                    user.EmailVerificationCode = emailJob.VerificationCode;
                    user.EmailVerificationDateTime = DateTime.Now.AddMinutes (10);
                    dbContext.SaveChanges();
                } else
                {
                    logger.LogError("Error while sending verification code, no user exists with given email id");
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error sending email for registration. Error: {1}", ex.Message);
            }
        }
    }
}
