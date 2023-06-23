using System.Data;
using PrisonBot.Loop;
using PrisonBot.Telegram;
using PrisonBot.Tools;
using RelationalDatabasesViaOOP;

namespace PrisonBot.Functional
{
    public sealed class CheckBot : ILoopObject
    {
        private readonly IDatabase _database;
        private readonly ITelegram _telegram;
        private readonly InformationStringFactory _informationStringFactory = new();

        public CheckBot(IDatabase database, ITelegram telegram)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
        }

        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.Message;
        
        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new InvalidOperationException("Can't get update");

            var arguments = updateInfo.Message!.Text!.GetCommandArguments();
            DataTable dataTable;
            
            if (arguments.Length == 0)
            {
                var userId = updateInfo.Message.ReplyToMessage == null ? updateInfo.Message!.From!.Id : updateInfo.Message!.ReplyToMessage!.From!.Id;
                dataTable = _database.SendReadingRequest($"SELECT * FROM passports WHERE id = {userId}");
            }
            else
            {
                var userName = string.Join(' ', arguments);
                dataTable = _database.SendReadingRequest(long.TryParse(userName, out long userId) 
                    ? $"SELECT * FROM passports WHERE id = {userId}" 
                    : $"SELECT * FROM passports WHERE name = '{userName}'");
            }

            var message = dataTable.Rows.Count == 0 ? "НЕ НАШЕЛ ТАКОГО ЧЕБУРЕКА" : _informationStringFactory.GetFor(dataTable);
            _telegram.SendMessage(message, updateInfo.Message!.Chat.Id, replyToMessageId: updateInfo.Message!.MessageId);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => updateInfo.Message!.Text != null && updateInfo.Message!.Text!.IsCommand("/check");
    }
}