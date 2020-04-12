using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Disqord.Bot;
using Qmmands;

namespace CalendarBot.Bot.Modules
{
    public class PingModule : DiscordModuleBase
    {
        [Command("ping")]
        public Task PingAsync() => ReplyAsync("Pong!");
    }
}
