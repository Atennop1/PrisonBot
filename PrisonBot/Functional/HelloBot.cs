using PrisonBot.Loop;
using PrisonBot.Telegram;
using PrisonBot.Tools;

namespace PrisonBot.Functional
{
    public sealed class HelloBot : ILoopObject
    {
        private readonly ITelegram _telegram;

        public HelloBot(ITelegram telegram) 
            => _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));

        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.Message;
        
        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new InvalidOperationException("Can't get update");

            var message = "ПРИВЕТ!\nЯ - БОТ, В КОТОРОМ МОЖНО ПОЧУВСТВОВАТЬ СЕБЯ КАК НА ЗОНЕ!\n" +
                          "ЧЕРЕЗ МЕНЯ ТЫ МОЖЕШЬ СМОТРЕТЬ ПАСПОРТА ЗАКЛЮЧЕННЫХ, ПРОСТО НАПИШИ КОМАНДУ /passport В ОТВЕТ НА СООБЩЕНИЕ ТОГО, ЧЕЙ ПАСПОРТ ТЫ ХОЧЕШЬ УВИДЕТЬ";
            
            _telegram.SendMessage(message, updateInfo.Message!.Chat.Id, replyToMessageId: updateInfo.Message.MessageId);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo)
            => updateInfo.Message!.Text != null && updateInfo.Message!.Text!.IsCommand("/start");
    }
}