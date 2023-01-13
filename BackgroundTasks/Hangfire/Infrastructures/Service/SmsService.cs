namespace Hangfire.Infrastructures.Service
{
    public class SmsService
    {
        private readonly ILogger _logger;
        public SmsService(ILogger<SmsService> _logger)
        {
            this._logger = _logger;
        }

        public void SendWelcomeSMS(string phoneNumber)
        {
            Thread.Sleep(2000);
            //کد های ارسال پیام خوش آمدگویی
            _logger.LogInformation($"An Welcome SMS Sent to Number{phoneNumber}");
        }
    }
}
