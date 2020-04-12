using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CalendarBot.Bot.Services;

namespace CalendarBot.Bot.Factories
{
    public class CalendarServiceFactory
    {
        public async Task<BotCalendarService> CreateCalendarService()
        {
            return new BotCalendarService();
        }
    }
}
