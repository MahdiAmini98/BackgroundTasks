namespace Hangfire.Infrastructures.Abstraction
{
    public interface IEmailSender
    {
        public void SendEmailToAllUsers(Guid sendMailId);
    }
}
