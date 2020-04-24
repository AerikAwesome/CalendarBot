using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CalendarBot.Bot.Utilities;
using CalendarBot.Data.Models;
using CalendarBot.Data.Repositories;
using Disqord.Bot;
using Microsoft.EntityFrameworkCore.Internal;
using Qmmands;

namespace CalendarBot.Bot.Modules
{
    [Name("Reminders")]
    [Group("reminder", "reminders", "r")]
    public class ReminderModule : DiscordModuleBase
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly ReminderCommandParser _commandParser;

        public ReminderModule(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
            _commandParser = new ReminderCommandParser();
        }

        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Reminder pong");
        }
        
        [Command("create")]
        public async Task CreateReminder([Remainder]string parameters)
        {
            _commandParser.Initialize(parameters);
            var channelParameter = _commandParser.GetChannelId(Context);
            if (channelParameter.Error != null)
            {
                await ReplyAsync(channelParameter.Error);
                return;
            }

            var startTime = _commandParser.GetStartTime();
            if (startTime.Error != null)
            {
                await ReplyAsync(startTime.Error);
                return;
            }

            var endTime = _commandParser.GetEndTime();
            if (endTime.Error != null)
            {
                await ReplyAsync(endTime.Error);
                return;
            }

            var message = _commandParser.GetMessage();
            if (message.Error != null)
            {
                await ReplyAsync(message.Error);
                return;
            }

            var reminder = new Reminder
            {
                Id = -1,
                ServerId = Context.Guild.Id.ToString(),
                ChannelId = channelParameter.Result,
                UserId = Context.User.Id.ToString(),
                Start = startTime.Result.Value,
                End = endTime.Result,
                Message = message.Result
            };
            await _reminderRepository.AddReminder(reminder);

            await ReplyAsync($"Reminder created!");
        }

        [Command("list", "ls")]
        public async Task ListReminders()
        {
            var reminders = await _reminderRepository.GetReminders(Context.Guild.Id.ToString());
            await ReplyAsync("Found the following reminders:\n" + string.Join('\n',
                reminders.Select(r => $"Reminder with id {r.Id} for {r.Start:dd-MM-yyyy HH:mm:ss} in channel {FindChannelName(r.ChannelId)}: {r.Message}")));
        }

        private string FindChannelName(string channelId)
        {
            return Context.Guild.Channels.SingleOrDefault(c => c.Value.Id.ToString() == channelId).Value?.Name;
        }
    }
}
