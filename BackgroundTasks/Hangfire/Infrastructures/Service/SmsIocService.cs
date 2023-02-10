using Hangfire.Infrastructures.Abstraction;

namespace Hangfire.Infrastructures.Service
{
    public class SmsIocService : ISmsIocService
    {
        private readonly ILogger<SmsIocService> _logger;
        public SmsIocService(ILogger<SmsIocService> logger)
        {
            _logger = logger;
        }
        public void HamrahAvalSms(string phoneNumber)
        {
            Thread.Sleep(8000);
            //کد های ارسال پیام خوش آمدگویی
            _logger.LogInformation($"An Welcome HamrahAvalSms Sent to Number{phoneNumber}");
        }

        public void IrancellSms(string phoneNumber)
        {
            Thread.Sleep(8000);
            //کد های ارسال پیام خوش آمدگویی
            _logger.LogInformation($"An Welcome IrancellSms Sent to Number{phoneNumber}");
        }

        public void RightelSms(string phoneNumber)
        {
            Thread.Sleep(8000);
            //کد های ارسال پیام خوش آمدگویی
            _logger.LogInformation($"An Welcome RightelSms Sent to Number{phoneNumber}");
        }
    }
}
