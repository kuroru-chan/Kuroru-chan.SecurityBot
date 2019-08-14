using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace KuroruChan.SecurityBot.Services
{
    public interface IUpdateService
    {
        Task EchoAsync(Update update);
    }
}
