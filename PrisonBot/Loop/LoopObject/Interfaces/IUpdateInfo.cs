using Telegram.BotAPI.AvailableTypes;

namespace PrisonBot.Loop
{
    public interface IUpdateInfo
    {
        TypeOfUpdate Type { get; }
        Message? Message { get; }
        CallbackQuery? CallbackQuery { get; }
    }
}