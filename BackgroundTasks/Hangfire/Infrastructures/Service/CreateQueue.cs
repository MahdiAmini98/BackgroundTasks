using Hangfire.Infrastructures.Abstraction;

namespace Hangfire.Infrastructures.Service
{
    public class CreateQueue : ICreateQueue
    {
        private readonly ILogger<CreateQueue> _logger;
        public CreateQueue(ILogger<CreateQueue> logger)
        {
            _logger= logger;
        }

        void ICreateQueue.CreateQueue()
        {
            Thread.Sleep(9000);
            _logger.LogInformation("CreateQueue");
        }
    }
}
