namespace Hangfire.Infrastructures.Abstraction
{
    public interface IExceptionJob
    {
        [AutomaticRetry(Attempts = 5, DelaysInSeconds = new[] {3,10,6})]
        public void RetriesJob();
    }
}
