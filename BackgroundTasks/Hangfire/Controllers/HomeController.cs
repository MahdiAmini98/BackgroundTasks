using Hangfire.Infrastructures.Service;
using Hangfire.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hangfire.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SmsService _smsService;

        public HomeController(ILogger<HomeController> logger, SmsService smsService)
        {
            _logger = logger;
            _smsService = smsService;
        }

        public IActionResult Index()
        {

            //BackgroundJob.Enqueue(methodCall:() => _smsService.SendWelcomeSMS("09220705761"));
            //BackgroundJob.Enqueue<SmsService>(p => p.SendWelcomeSMS("09220705761"));
            return View();
        }

        #region Jobs
        
        public IActionResult FireAndForgetJob()
        {

            //BackgroundJob.Enqueue(methodCall:() => _smsService.SendWelcomeSMS("09220705761"));
            BackgroundJob.Enqueue<SmsService>(p => p.SendWelcomeSMS("09220705761"));
            return RedirectToAction("Index");
        }

        public IActionResult DelayedJob()
        {
            BackgroundJob.Schedule<EmailService>(p => p.SendDiscountCode("classicus.ma@gmail.com"), TimeSpan.FromMinutes(1));
            return RedirectToAction("Index");
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}