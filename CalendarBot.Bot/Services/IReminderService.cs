using System.Threading.Tasks;

namespace CalendarBot.Bot.Services
{
    public interface IReminderService
    {
        Task SendReminder(int reminderId);
    }
}