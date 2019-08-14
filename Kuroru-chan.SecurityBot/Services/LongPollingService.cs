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
        private readonly IBotService _botService;
        private readonly IUpdateService _updateService;
        public LongPollingService(IBotService botService, IUpdateService updateService, ILogger<LongPollingService> logger)
        {
            _logger = logger;
            _botService = botService;
            _updateService = updateService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            try
            {
                _logger.LogInformation("[DEBUG]Bot program is running under long polling mode");
                //Bind bot handler
                _botService.Client.OnUpdate += async (sender, e) =>
                {
                    _logger.LogInformation("[DEBUG]Pulling updates from Telegram");
                    await _updateService.EchoAsync(e.Update);
                };
                //Start receiving
                _botService.Client.StartReceiving();
                await Task.FromCanceled(stoppingToken)
                    .ContinueWith(task =>
                    {
                        //End receiving
                        _logger.LogInformation("[DEBUG]Exiting long polling mode");
                        _botService.Client.StopReceiving();
                    });
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
                    _botService.Client.StopReceiving();
                }
            }
        }
    }
}
