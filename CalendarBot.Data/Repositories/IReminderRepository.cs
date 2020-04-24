using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarBot.Data.Models;

namespace CalendarBot.Data.Repositories
{
    public interface IReminderRepository
    {
        Task<IEnumerable<Reminder>> GetReminders(string serverId);
        Task<Reminder> GetReminder(int reminderId);
        Task<Reminder> AddReminder(Reminder reminder);
        Task UpdateReminder(Reminder reminder);
        Task RemoveReminder(int id);
    }
}