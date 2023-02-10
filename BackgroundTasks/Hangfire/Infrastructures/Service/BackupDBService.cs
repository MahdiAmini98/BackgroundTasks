namespace Hangfire.Infrastructures.Service
{
    public class BackupDBService
    {
        private readonly ILogger _logger;
        public BackupDBService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public void BackupDB()
        {
            Thread.Sleep(120000);
            _logger.LogInformation("Backup DB is Done...");
        }

        public void RestoreDB()
        {
            Thread.Sleep(120000);
            _logger.LogInformation("Restore DB is Done...");
        }
    }
}
