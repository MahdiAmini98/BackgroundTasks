﻿using Hangfire.Infrastructures.Abstraction;
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

        public IActionResult RecurringJob()
        {
            Hangfire.RecurringJob.AddOrUpdate<EmailService>("Article-1", p => p.SendArticlesToUsers("Classicus.ma@gmail.com"), Cron.Minutely()); 
            return RedirectToAction("Index");
        }         

        public IActionResult DeleteRecurringJob()
        {
            Hangfire.RecurringJob.RemoveIfExists("Article-1");
            return RedirectToAction("Index");
        }

        public IActionResult RunRecurringJob()
        {
            Hangfire.RecurringJob.TriggerJob("Article-1");
            return RedirectToAction("Index");
        }

        public IActionResult ContinuationsJob()
        {
          var job_Id =  BackgroundJob.Enqueue<BackupDBService>(p => p.BackupDB());
            BackgroundJob.ContinueJobWith<BackupDBService>(job_Id, p => p.RestoreDB(),JobContinuationOptions.OnlyOnSucceededState);
            return RedirectToAction("Index");
        }

        public IActionResult IOCSms()
        {
            BackgroundJob.Enqueue<ISmsIocService>(p => p.IrancellSms("09220705761"));
            BackgroundJob.Enqueue<ISmsIocService>(p => p.HamrahAvalSms("09220705761"));
            BackgroundJob.Enqueue<ISmsIocService>(p => p.RightelSms("09220705761"));

            return RedirectToAction("Index");
        }

        public IActionResult CreateException()
        {
            BackgroundJob.Enqueue<IExceptionJob>(p => p.RetriesJob());
            return RedirectToAction("Index");
        }

        public IActionResult ChangeDisplayNameJob()
        {
            BackgroundJob.Enqueue<IChangeDisplayNameJob>(p => p.ChangeDisplayNameJob());
            return RedirectToAction("Index");
        }

        public IActionResult CreateQueue()
        {
            BackgroundJob.Enqueue<ICreateQueue>(p => p.CreateQueue());
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