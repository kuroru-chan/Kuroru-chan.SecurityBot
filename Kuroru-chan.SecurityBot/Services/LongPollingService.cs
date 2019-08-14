using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KuroruChan.SecurityBot.Services
{
    /// <summary>
    /// Used for long polling mode of Telegram updates
    /// </summary>
    public class LongPollingService : BackgroundService
    {
        private readonly ILogger _logger;
        public LongPollingService(ILogger<LongPollingService> logger)
        {
            this._logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            try
            {
                _logger.LogInformation("[DEBUG]Bot program is running under long polling mode");

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(5000, stoppingToken);
                    _logger.LogInformation("[DEBUG]Pulling updates from Telegram");
                }
                _logger.LogInformation("[DEBUG]Exiting long polling mode");
            }
            catch (Exception ex)
            {
                if (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"[DEBUG]Error:{ex.Message}");
                }
                else
                {
                    _logger.LogInformation("[DEBUG]Exiting long polling mode");
                }
            }
        }

    }
}
