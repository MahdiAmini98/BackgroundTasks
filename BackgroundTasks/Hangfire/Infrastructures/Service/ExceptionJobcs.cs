using Hangfire.Infrastructures.Abstraction;

namespace Hangfire.Infrastructures.Service
{
    public class ExceptionJobcs : IExceptionJob
    {
        public void RetriesJob()
        {
            throw new NotImplementedException();
        }
    }
}
