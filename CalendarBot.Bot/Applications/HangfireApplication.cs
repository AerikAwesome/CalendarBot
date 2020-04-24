using System;
using System.Threading;
using System.Threading.Tasks;
using CalendarBot.Bot.Utilities;
using Hangfire;

namespace CalendarBot.Bot.Applications
{

    public class HangfireApplication : IApplication
    {
        public async Task Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using (var server = new BackgroundJobServer())
                    {
                        await cancellationToken;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
