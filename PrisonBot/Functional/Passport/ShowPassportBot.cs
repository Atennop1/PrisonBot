using System.Data;
using PrisonBot.Loop;
using PrisonBot.Telegram;
using PrisonBot.Tools;
using RelationalDatabasesViaOOP;

namespace PrisonBot.Functional
{
    public sealed class ShowPassportBot : ILoopObject
    {
        private readonly IDatabase _database;
        private readonly ITelegram _telegram;
        private readonly InformationStringFactory _informationStringFactory = new();

        public ShowPassportBot(IDatabase database, ITelegram telegram)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
        }

        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.Message;

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => updateInfo.Message!.Text != null && updateInfo.Message!.Text!.IsCommand("/passport");

        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new InvalidOperationException("Can't get update");

            var arguments = updateInfo.Message!.Text!.GetCommandArguments();
            var dataTable = arguments.Length == 0 ? GetTableWhenZeroArguments(updateInfo) : GetTableWhenNonZeroArguments(arguments);

            var message = dataTable.Rows.Count == 0 ? "НЕ НАШЕЛ ПАСПОРТ ЭТОГО ЧЕЛИКА" : _informationStringFactory.GetFor(dataTable);
            _telegram.SendMessage(message, updateInfo.Message!.Chat.Id, replyToMessageId: updateInfo.Message!.MessageId);
        }

        private DataTable GetTableWhenNonZeroArguments(string[] arguments)
        {
            DataTable dataTable;
            var nickname = string.Join(' ', arguments);

            if (long.TryParse(nickname, out long userId))
            {
                dataTable = _database.SendReadingRequest($"SELECT * FROM passports WHERE ids LIKE '%{userId}%'");
                if (dataTable.Rows.Count == 0)
                    return dataTable;
                
                var tables = new List<DataTable> { dataTable, _database.SendReadingRequest($"SELECT * FROM statuses WHERE ids LIKE '%{userId}%'") };
                dataTable = tables.RightMerge("ids");
            }
            else
            {
                dataTable = _database.SendReadingRequest($"SELECT * FROM passports WHERE UPPER(nickname) = UPPER('{nickname}')");
                if (dataTable.Rows.Count == 0)
                    return dataTable;
                
                var tables = new List<DataTable> { dataTable, _database.SendReadingRequest($"SELECT * FROM statuses WHERE ids LIKE '%{dataTable.Rows[0]["ids"]}%'") };
                dataTable = tables.RightMerge("ids");
            }

            return dataTable;
        }

        private DataTable GetTableWhenZeroArguments(IUpdateInfo updateInfo)
        {
            var userId = updateInfo.Message!.ReplyToMessage == null ? updateInfo.Message!.From!.Id : updateInfo.Message!.ReplyToMessage!.From!.Id;
            var dataTable = _database.SendReadingRequest($"SELECT * FROM passports WHERE ids LIKE '%{userId}%'");
            
            var tables = new List<DataTable> {dataTable, _database.SendReadingRequest($"SELECT * FROM statuses WHERE ids LIKE '%{userId}%'") };
            dataTable = tables.RightMerge("ids");
            return dataTable;
        }
    }
}