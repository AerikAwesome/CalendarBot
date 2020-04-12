using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CalendarBot.Bot.Services;
using Disqord.Bot;
using Qmmands;

namespace CalendarBot.Bot.Modules
{
    public class PingModule : DiscordModuleBase
    {
        private IUserService _userService;
        public PingModule(IUserService userService)
        {
            _userService = userService;
        }

        [Command("ping")]
        public Task PingAsync() => ReplyAsync("Pong!");

        [Command("user")]
        public async Task UserAsync()
        {
            var user = await _userService.GetUserFromDiscordId("x");

            await ReplyAsync(user.ToString());
        }
    }
}
