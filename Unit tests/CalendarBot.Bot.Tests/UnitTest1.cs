using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CalendarBot.Bot.Tests
{
    public class UnitTest1
    {
        private static readonly string[] _usedKeys = new[] { "-s", "-e", "-c", "-m" };

        [Fact]
        public void Test1()
        {
            var result = GetParameters(
                "create -s 2020-04-22 19:00:00 -e 2020-04-22 20:00:00 -c #big-brains -m This is a test message - ignore it and-others");
        }


        private Dictionary<string, string> GetParameters(string fullCommand)
        {
            var keyIndexDictionary = new Dictionary<string, int>();
            foreach (var usedKey in _usedKeys)
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
                var nextIndex = i + 1 < orderedIndexes.Count() ? orderedIndexes.ElementAt(i + 1).Value : fullCommand.Length - 1;

                var actualIndex = keyIndexPair.Value + keyIndexPair.Key.Length;

                var text = fullCommand.Substring(actualIndex, nextIndex - actualIndex).Trim();

                result.Add(keyIndexPair.Key, text);
            }

            return result;
        }

    }
}
