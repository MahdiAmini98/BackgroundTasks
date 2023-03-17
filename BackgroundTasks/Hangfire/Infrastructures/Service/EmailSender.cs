using Hangfire.Data;
using Hangfire.Infrastructures.Abstraction;

namespace Hangfire.Infrastructures.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly ApplicationDbContext _context;
        public EmailSender(ApplicationDbContext context)
        {
            _context= context;
        }
        public void SendEmailToAllUsers(Guid sendMailId)
        {
           var sendMail = _context.SendMail.Find(sendMailId);
            ArgumentNullException.ThrowIfNull(sendMail);

            for (int i = 0; i < 10000; i++)
            {
                Thread.Sleep(10);
            }
            sendMail.SendMailStatus = Models.Entities.SendMailStatus.Done;
            sendMail.EndDate = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
