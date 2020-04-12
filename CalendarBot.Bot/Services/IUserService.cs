using System.Threading.Tasks;

namespace CalendarBot.Bot.Services
{
    public interface IUserService
    {
        Task<object> GetUserFromDiscordId(string discordId);
    }
}