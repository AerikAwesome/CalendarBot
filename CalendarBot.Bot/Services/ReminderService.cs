using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CalendarBot.Data.Repositories;

namespace CalendarBot.Bot.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public async Task SendReminder(int reminderId)
        {
            throw new NotImplementedException();
        }
    }
}
