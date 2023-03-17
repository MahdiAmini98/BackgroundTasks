using Hangfire.Models.Entities;

namespace Hangfire.Models.Dto
{
    public class MailDto
    {
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!; 
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public SendMailStatus SendMailStatus { get; set; }
    }
}
