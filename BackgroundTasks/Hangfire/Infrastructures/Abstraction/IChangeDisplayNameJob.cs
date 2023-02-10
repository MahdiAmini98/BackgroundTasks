namespace Hangfire.Infrastructures.Abstraction
{
    public interface IChangeDisplayNameJob
    {
        [JobDisplayName("مهدی امینی")]
        public void ChangeDisplayNameJob();
    }
}
