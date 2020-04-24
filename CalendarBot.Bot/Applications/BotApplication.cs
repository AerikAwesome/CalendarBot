using System;
using System.Threading;
using System.Threading.Tasks;
using Disqord.Bot;

namespace CalendarBot.Bot.Applications
{
    public class BotApplication : IApplication
    {
        private readonly DiscordBot _bot;
        public BotApplication(DiscordBot bot)
        {
            _bot = bot;
        }
        public async Task Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await _bot.RunAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    var x = 0;
                }
            }
        }
    }
}
