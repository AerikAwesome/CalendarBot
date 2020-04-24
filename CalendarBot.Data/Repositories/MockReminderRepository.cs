using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendarBot.Data.Models;

namespace CalendarBot.Data.Repositories
{
    public class MockReminderRepository : IReminderRepository
    {
        private static readonly Dictionary<int, Reminder> ReminderDictionary = new Dictionary<int, Reminder>();

        public async Task UpdateReminder(Reminder reminder)
        {
            ReminderDictionary[reminder.Id] = reminder;
        }

        public async Task RemoveReminder(int id)
        {
            ReminderDictionary.Remove(id);
        }

        public async Task<IEnumerable<Reminder>> GetReminders(string serverId)
        {
            return ReminderDictionary.Values.Where(r => r.ServerId == serverId);
        }

        public async Task<Reminder> GetReminder(int reminderId)
        {
            return ReminderDictionary[reminderId];
        }

        public async Task<Reminder> AddReminder(Reminder reminder)
        {
            if (reminder.Id <= 0)
            {
                reminder.Id = ReminderDictionary.Keys.DefaultIfEmpty(-1).Max() + 1;
            }
            ReminderDictionary.Add(reminder.Id, reminder);
            return reminder;
        }
    }
}
