using System;
using System.Threading.Tasks;
using CalendarBot.Bot.Factories;
using CalendarBot.Bot.Modules;
using CalendarBot.Bot.Services;
using CalendarBot.Data;
using Disqord;
using Disqord.Bot;
using Disqord.Bot.Prefixes;
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

            var application = serviceProvider.GetService<IBotApplication>();

            await application.Run();

        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var config = BuildConfiguration();

            services.AddSingleton(config);
            services.ConfigureDatabaseConnection(config);

            //Add services
            services.AddSingleton<IUserService, UserService>();

            services.AddSingleton(provider =>
            {
                var prefixProvider = new DefaultPrefixProvider()
                    .AddPrefix('*')
                    .AddMentionPrefix();

                var bot = new DiscordBot(TokenType.Bot, config["Discord:BotToken"], prefixProvider,
                    new DiscordBotConfiguration {ProviderFactory = _ => provider});

                bot.AddModule<PingModule>();

                return bot;
            });
            services.AddTransient<IBotApplication, BotApplication>();

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
