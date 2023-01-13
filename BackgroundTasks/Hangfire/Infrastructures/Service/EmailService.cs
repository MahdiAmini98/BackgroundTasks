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
    }
}
