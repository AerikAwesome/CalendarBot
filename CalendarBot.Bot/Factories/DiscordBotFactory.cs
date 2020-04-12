using System;
using System.Collections.Generic;
using System.Text;
using CalendarBot.Bot.Modules;
using Disqord;
using Disqord.Bot;
using Disqord.Bot.Prefixes;

namespace CalendarBot.Bot.Factories
{
    public class DiscordBotFactory
    {
        public DiscordBot CreateBot(string token)
        {
            var prefixProvider = new DefaultPrefixProvider()
                .AddPrefix('*')
                .AddMentionPrefix();

            var bot = new DiscordBot(TokenType.Bot, token, prefixProvider);

            bot.AddModule<PingModule>();

            return bot;
        }
    }
}
