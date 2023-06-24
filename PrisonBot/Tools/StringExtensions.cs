using PrisonBot.Core;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;

namespace PrisonBot.Tools
{
    public static class StringExtensions
    {
        private static readonly BotClient _client = new TelegramClientFactory().Create();
        
        public static bool IsCommand(this string str, string command) 
            => str.StartsWith(command) || str.StartsWith($"{command}@{_client.GetMe().Username}");

        public static string[] GetCommandArguments(this string str) 
            => str.Split(' ').Skip(1).ToArray();
    }
}