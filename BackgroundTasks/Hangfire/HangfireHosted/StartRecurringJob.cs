using Hangfire.Infrastructures.Service;

namespace Hangfire.HangfireHosted
{
    public class StartRecurringJob : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Hangfire.RecurringJob.AddOrUpdate<EmailService>("Article-LifeTimeApp", p => p.SendArticlesToUsers("Classicus.ma@gmail.com"), Cron.Daily());
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
