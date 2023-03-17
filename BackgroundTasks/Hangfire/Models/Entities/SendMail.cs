#nullable disable
namespace Hangfire.Models.Entities
{
    public class SendMail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public SendMailStatus SendMailStatus { get; set; }
    }

    public enum SendMailStatus
    {
        Done = 0, 
        Sending = 1,
    }
}
