using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalendarBot.Bot.Applications
{
    public interface IApplication
    {
        Task Run(CancellationToken cancellationToken);
    }
}
