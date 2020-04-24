using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CalendarBot.Data.Models;

namespace CalendarBot.Data.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public ReminderRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Reminder>> GetReminders(string serverId)
        {
            throw new NotImplementedException();
        }

        public async Task<Reminder> GetReminder(int reminderId)
        {
            throw new NotImplementedException();
        }

        public Task<Reminder> AddReminder(Reminder reminder)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReminder(Reminder reminder)
        {
            throw new NotImplementedException();
        }

        public Task RemoveReminder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
