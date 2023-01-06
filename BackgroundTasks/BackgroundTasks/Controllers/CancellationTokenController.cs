using Microsoft.AspNetCore.Mvc;

namespace BackgroundTasks.Controllers
{
    public class CancellationTokenController : Controller
    {
          private readonly ILogger<CancellationTokenController> _logger;

        public CancellationTokenController(ILogger<CancellationTokenController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        //CanceletionToken
        public async Task<string> CancellationToken(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Cancellation Token............");

            for (int i = 0; i < 5; i++)
            {
                // cancellationToken.ThrowIfCancellationRequested();
                _logger.LogInformation(i.ToString());
                //await Task.Delay(1000, cancellationToken); 

                if (cancellationToken.IsCancellationRequested)
                {
                    throw new Exception("IsCancellationRequested == True");
                }
                await Task.Delay(1000);
            }

            string message = "Finished Cancellation Token............";
            _logger.LogInformation(message);
            return message;
        }
    }
}
