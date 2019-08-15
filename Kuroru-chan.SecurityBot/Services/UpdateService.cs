using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace KuroruChan.SecurityBot.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;
        private readonly ILogger<UpdateService> _logger;
        private readonly IAnonymousService _anonymousService;

        public UpdateService(IBotService botService, IAnonymousService anonymousService, ILogger<UpdateService> logger)
        {
            _botService = botService;
            _anonymousService = anonymousService;
            _logger = logger;
            _anonymousService.GenerateAnoymousNames();
        }

        public async Task EchoAsync(Update update)
        {
            if (update.Type != UpdateType.Message)
            {
                return;
            }
            var message = update.Message;
            if (message.Type == MessageType.Text)
            {
                var chatId = message.Chat.Id;
                var msgText = message.Text;
                var userId = message.From.Id;
                await _botService.Client.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                await _botService.Client.SendTextMessageAsync(chatId, $"*{_anonymousService.GetAnoymousName(userId)}:* \n{msgText}", parseMode: ParseMode.Markdown);
                _logger.LogInformation($"[Message]{message.From.Username}: {message.Text}");
            }


        }
    }
}
