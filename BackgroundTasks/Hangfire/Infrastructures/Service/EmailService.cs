using System;

namespace Hangfire.Infrastructures.Service
{
    public class EmailService
    {
        private readonly ILogger _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger= logger;
        }

        public void SendWelcomeEmail(string email)
        {
            Thread.Sleep(3000);
            //کدهای ارسال ایمیل خوش آمدگویی کاربر
            _logger.LogInformation($"An Welcome email was sent to {email}");

        }

        public void SendDiscountCode(string email)
        {
            Thread.Sleep(3000);
            Random random = new Random();
            
            _logger.LogInformation($"Discount Code {random.Next(500, 50000)} send to email {email}");
        }

        public void SendArticlesToUsers(string email)
        {
            Thread.Sleep(6000);
            Random random = new Random();

            _logger.LogInformation($"Article Number {random.Next(85, 8500)} send to email {email}");

        }
    }
}
