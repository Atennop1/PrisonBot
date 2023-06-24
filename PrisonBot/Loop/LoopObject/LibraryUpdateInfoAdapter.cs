using Telegram.BotAPI;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;

namespace PrisonBot.Loop
{
    public sealed class LibraryUpdateInfoAdapter : IUpdateInfo
    {
        private readonly Update _update;

        public LibraryUpdateInfoAdapter(Update update) 
            => _update = update ?? throw new ArgumentNullException(nameof(update));
        
        public Message? Message 
            => _update.Message;
        
        public CallbackQuery? CallbackQuery 
            => _update.CallbackQuery;

        public TypeOfUpdate Type
        {
            get
            {
                return _update.Type switch
                {
                    UpdateType.Message => TypeOfUpdate.Message,
                    UpdateType.CallbackQuery => TypeOfUpdate.ButtonCallback,
                    _ => (TypeOfUpdate)(-1)
                };
            }
        }
    }
}