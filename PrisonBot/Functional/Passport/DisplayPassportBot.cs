using PrisonBot.Loop;
using PrisonBot.Telegram;
using PrisonBot.Tools;

namespace PrisonBot.Functional
{
    public sealed class DisplayPassportBot : ILoopObject
    {
        private readonly ITelegram _telegram;
        private readonly IInformationTableFactory _informationTableFactory;
        private readonly InformationStringFactory _informationStringFactory = new();

        public DisplayPassportBot(ITelegram telegram, IInformationTableFactory informationTableFactory)
        {
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
            _informationTableFactory = informationTableFactory ?? throw new ArgumentNullException(nameof(informationTableFactory));
        }
        
        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.Message;

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => updateInfo.Message!.Text != null && updateInfo.Message!.Text!.IsCommand("/passport");

        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new InvalidOperationException("Can't get update");

            var dataTable = _informationTableFactory.Create(updateInfo);
            var message = dataTable.Rows.Count == 0 ? "НЕ НАШЕЛ ПАСПОРТ ЭТОГО ЧЕЛИКА" : _informationStringFactory.GetFor(dataTable);
            
            _telegram.SendMessage(message, updateInfo.Message!.Chat.Id, replyToMessageId: updateInfo.Message!.MessageId);
        }
    }
}