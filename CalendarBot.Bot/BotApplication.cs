using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CalendarBot.Bot.Factories;
using Disqord.Bot;

namespace CalendarBot.Bot
{
    public interface IBotApplication
    {
        Task Run();
    }

    public class BotApplication : IBotApplication
    {
        private readonly DiscordBot _bot;
        public BotApplication(DiscordBot bot)
        {
            _bot = bot;
        }
        public async Task Run()
        {
            await _bot.RunAsync();
        }
    }
}
