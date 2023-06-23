namespace PrisonBot.Tools
{
    public static class StringExtensions
    {
        public static bool IsCommand(this string str, string command) 
            => str.StartsWith(command) || str.StartsWith($"{command}@CookiesAtennop_bot");

        public static string[] GetCommandArguments(this string str) 
            => str.Split(' ').Skip(1).ToArray();
    }
}