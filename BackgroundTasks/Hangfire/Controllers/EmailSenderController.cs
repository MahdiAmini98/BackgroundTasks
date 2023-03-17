using Hangfire.Data;
using Hangfire.Infrastructures.Abstraction;
using Hangfire.Models.Dto;
using Hangfire.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace Hangfire.Controllers
{
    public class EmailSenderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBackgroundJobClient _backgroundJob;

        public EmailSenderController(ApplicationDbContext context, IBackgroundJobClient backgroundJob)
        {
            _context = context;
            _backgroundJob = backgroundJob;
        }

        public IActionResult Index()
        {
            var emails = _context.SendMail.OrderByDescending(x => x.StartDate).ToList();
            return View(emails);
        }

        public IActionResult Detail(Guid id) 
        {
            var email = _context.SendMail.Find(id);
            ArgumentNullException.ThrowIfNull(email);
            return View(email);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MailDto model)
        {
            ArgumentNullException.ThrowIfNull(model);
            SendMail sendMail = new()
            {
                Subject = model.Subject,
                Body = model.Body
            };
            _context.SendMail.Add(sendMail);
            _context.SaveChanges();
            _backgroundJob.Enqueue<IEmailSender>(x => x.SendEmailToAllUsers(sendMail.Id));
            return RedirectToAction("Index");
        }
    }
}
