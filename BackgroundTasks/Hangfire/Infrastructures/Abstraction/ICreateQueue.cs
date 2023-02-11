namespace Hangfire.Infrastructures.Abstraction
{
    public interface ICreateQueue
    {
        [Queue("create_queue1")]
        public void CreateQueue();
    }
}
