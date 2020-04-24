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
        private readonly IUserService _userService;
        public PingModule(IUserService userService)
        {
            _userService = userService;
        }

        [Command("ping")]
        public Task PingAsync() => ReplyAsync("Pong!");

        [Command("user")]
        public async Task UserAsync()
        {
            try
            {
                var user = await _userService.GetUserFromDiscordId(Context.User.Id.ToString());

                await ReplyAsync(user.ToString());
            }
            catch (Exception ex)
            {
                await ReplyAsync($"Message: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }
}
