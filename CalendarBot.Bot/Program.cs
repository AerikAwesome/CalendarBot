using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarBot.Bot.Applications;
using CalendarBot.Bot.Decorators;
using CalendarBot.Bot.Modules;
using CalendarBot.Bot.Services;
using CalendarBot.Bot.Utilities;
using CalendarBot.Data;
using CalendarBot.Data.Repositories;
using Disqord;
using Disqord.Bot;
using Disqord.Bot.Prefixes;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarBot.Bot
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            var cancellationToken = new CancellationToken();
            
            var applications = serviceProvider.GetServices<IApplication>();

            var tasks = applications.Select(a => a.Run(cancellationToken));

            await Task.WhenAll(tasks);
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var config = BuildConfiguration();

            services.AddSingleton(config);
            services.ConfigureDatabaseConnection(config);

            //configure Hangfire
            GlobalConfiguration.Configuration
                .UseMemoryStorage();

            //Add services
            services.AddSingleton<IUserService, UserService>();

            services.AddScoped<IReminderRepository, MockReminderRepository>();
            services.Decorate<IReminderRepository, HangfireReminderDecorator>();

            services.AddSingleton(provider =>
            {
                var prefixProvider = new DefaultPrefixProvider()
                    .AddPrefix('*')
                    .AddMentionPrefix();

                var bot = new DiscordBot(TokenType.Bot, config["Discord:BotToken"], prefixProvider,
                    new DiscordBotConfiguration {ProviderFactory = _ => provider});

                bot.AddModule<PingModule>();
                bot.AddModule<ReminderModule>();

                return bot;
            });

            services.Scan(scan => scan
                .FromAssemblyOf<Program>()
                .AddClasses(classes => classes
                    .AssignableTo<IApplication>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            
            return services;
        }

        private static IConfiguration BuildConfiguration()
        {
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                devEnvironmentVariable.ToLower() == "development";

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");

            if (isDevelopment)
            {
                builder.AddUserSecrets<Program>();
            }

            return builder.Build();
        }
    }
}
