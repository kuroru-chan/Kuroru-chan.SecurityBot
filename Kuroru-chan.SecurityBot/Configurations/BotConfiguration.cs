using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuroruChan.SecurityBot.Configurations
{
    public class BotConfiguration
    {
        public string BotToken { get; set; }
        public string Socks5Host { get; set; }
        public int Socks5Port { get; set; }
    }
}
