using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disqord;
using Disqord.Bot;
using Microsoft.EntityFrameworkCore.Internal;

namespace CalendarBot.Bot.Utilities
{
    public class ReminderCommandParser
    {
        private static readonly string[] UsedKeys = { "-s", "-e", "-c", "-m" };
        private Dictionary<string, string> _parameters;

        public void Initialize(string parameterString)
        {
            _parameters = GetParameters(parameterString);
        }

        public (string Result, string Error) GetChannelId(DiscordCommandContext context)
        {
            if (_parameters.TryGetValue("-c", out var channelName))
            {
                var result = context.Guild.Channels.FirstOrDefault(c =>
                    c.Value.Name == channelName || c.Value.Id.ToString() == channelName || $"<#{c.Value.Id}>" == channelName).Value.Id.ToString();

                if (result == null)
                {
                    return (null, "Channel not found");
                }
                else
                {
                    return (result, null);
                }
            }
            else
            {
                return (context.Channel.Id.ToString(), null);
            }
        }
        public (DateTime? Result, string Error) GetStartTime()
        {
            if (_parameters.TryGetValue("-s", out var dateString))
            {
                if (DateTime.TryParse(dateString, out var result))
                {
                    return (result, null);
                }
                else
                {
                    return (null, "Could not parse start time");
                }
            }
            else
            {
                return (null, "No start time parameter found, use -s {date/time}");
            }
        }

        public (DateTime? Result, string Error) GetEndTime()
        {
            if (_parameters.TryGetValue("-e", out var dateString))
            {
                if (DateTime.TryParse(dateString, out var result))
                {
                    return (result, null);
                }
                else
                {
                    return (null, "Could not parse end time");
                }
            }
            else
            {
                return (null, null);
            }
        }

        public (string Result, string Error) GetMessage()
        {
            if (_parameters.TryGetValue("-m", out var message))
            {
                return (message, null);
            }
            else
            {
                return (null, "No message found");
            }
        }
        
        private Dictionary<string, string> GetParameters(string fullCommand)
        {
            var keyIndexDictionary = new Dictionary<string, int>();
            foreach (var usedKey in UsedKeys)
            {
                var index = fullCommand.IndexOf(usedKey, StringComparison.InvariantCultureIgnoreCase);
                if (index >= 0)
                {
                    keyIndexDictionary.Add(usedKey, index);
                }
            }
            var result = new Dictionary<string, string>();
            var orderedIndexes = keyIndexDictionary.OrderBy(p => p.Value);
            for (var i = 0; i < orderedIndexes.Count(); i++)
            {
                var keyIndexPair = orderedIndexes.ElementAt(i);
                var nextIndex = i + 1 < orderedIndexes.Count() ? orderedIndexes.ElementAt(i + 1).Value : fullCommand.Length;

                var actualIndex = keyIndexPair.Value + keyIndexPair.Key.Length;

                var text = fullCommand.Substring(actualIndex, nextIndex - actualIndex).Trim();

                result.Add(keyIndexPair.Key, text);
            }

            return result;
        }
    }
}
