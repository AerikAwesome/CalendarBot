using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CalendarBot.Bot.Services;
using CalendarBot.Data.Models;
using CalendarBot.Data.Repositories;
using Hangfire;

namespace CalendarBot.Bot.Decorators
{
    public class HangfireReminderDecorator : IReminderRepository
    {
        private readonly IReminderRepository _reminderRepository;

        public HangfireReminderDecorator(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public Task<IEnumerable<Reminder>> GetReminders(string serverId)
        {
            return _reminderRepository.GetReminders(serverId);
        }

        public Task<Reminder> GetReminder(int reminderId)
        {
            return _reminderRepository.GetReminder(reminderId);
        }

        public async Task<Reminder> AddReminder(Reminder reminder)
        {
            var savedReminder = await _reminderRepository.AddReminder(reminder);
            var jobId = BackgroundJob.Schedule<IReminderService>(x => x.SendReminder(savedReminder.Id), savedReminder.Start);
            savedReminder.JobId = jobId;
            await _reminderRepository.UpdateReminder(savedReminder);
            return savedReminder;
        }

        public async Task UpdateReminder(Reminder reminder)
        {
            BackgroundJob.Delete(reminder.JobId);
            reminder.JobId = BackgroundJob.Schedule<IReminderService>(x => x.SendReminder(reminder.Id), reminder.Start);
            await _reminderRepository.UpdateReminder(reminder);
        }

        public async Task RemoveReminder(int id)
        {
            var reminder = await _reminderRepository.GetReminder(id);
            BackgroundJob.Delete(reminder.JobId);
            await _reminderRepository.RemoveReminder(id);
        }
    }
}
