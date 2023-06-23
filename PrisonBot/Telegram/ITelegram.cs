using Telegram.BotAPI.AvailableTypes;

namespace PrisonBot.Telegram
{
    public interface ITelegram
    {
        void SendMessage(string text, long id, ReplyMarkup replyMarkup = null!, long? replyToMessageId = null!);
    }
}