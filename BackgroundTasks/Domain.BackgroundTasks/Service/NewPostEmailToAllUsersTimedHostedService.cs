using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BackgroundTasks.Service
{
    public class NewPostEmailToAllUsersTimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer = null;
        private int _executionCount = 0;
        private readonly ILogger<NewPostEmailToAllUsersTimedHostedService> _logger;
        public NewPostEmailToAllUsersTimedHostedService(ILogger<NewPostEmailToAllUsersTimedHostedService> logger)
        {
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background Task is Starting......");

            _timer = new Timer(SendEmail, null, getJobRunDelay("18:16"), TimeSpan.FromSeconds(6));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background Task is Stoping......");
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void SendEmail(object? state)
        {
            var count = Interlocked.Increment(ref _executionCount);
            //Get newPost in DataBase
            //Send Email For All Users

            _logger.LogInformation($"Email Sent ... count:{count}");
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        private TimeSpan getScheduledParsedTime(string jobStartTime)
        {
            string[] formats = { @"hh\:mm\:ss", "hh\\:mm" };
            TimeSpan.TryParseExact(jobStartTime, formats, CultureInfo.InvariantCulture, out TimeSpan scheduledTimeSpan);
            return scheduledTimeSpan;
        }

        private TimeSpan getJobRunDelay(string jobStartTime)
        {
            TimeSpan scheduledParsedTime = getScheduledParsedTime(jobStartTime); 
            TimeSpan curentTimeOfTheDay = TimeSpan.Parse(DateTime.Now.TimeOfDay.ToString("hh\\:mm"));
            TimeSpan delayTime = scheduledParsedTime >= curentTimeOfTheDay
                ? scheduledParsedTime - curentTimeOfTheDay
                : new TimeSpan(24, 0, 0) - curentTimeOfTheDay + scheduledParsedTime;
            return delayTime;
        }
    }
}
