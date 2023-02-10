using Hangfire.Infrastructures.Abstraction;

namespace Hangfire.Infrastructures.Service
{
    public class ChangeDisplayNameJob : IChangeDisplayNameJob
    {
        private readonly ILogger<ChangeDisplayNameJob> _logger;
        public ChangeDisplayNameJob(ILogger<ChangeDisplayNameJob> logger)
        {
            _logger= logger;
        }
        void IChangeDisplayNameJob.ChangeDisplayNameJob()
        {
            _logger.LogInformation("Change Display Name Job");
        }
    }
}
