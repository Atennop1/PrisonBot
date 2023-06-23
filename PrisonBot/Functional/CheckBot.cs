using System.Text;
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
            
            var dataTable = _database.SendReadingRequest("SELECT * FROM passports");
            var finalMessageStringBuilder = new StringBuilder();
            finalMessageStringBuilder.Append("ПАСПОРТА МКС:\n\n");

            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                finalMessageStringBuilder.Append($"ИМЯ: {((string)dataTable.Rows[i]["name"]).ToUpper()}\n" +
                                                 $"СКОКО ЛЕТ В ЗОНЕ: {((int)dataTable.Rows[i]["how_many_years"]).ToString().ToUpper()}\n" +
                                                 $"СТАТУС: {((string)dataTable.Rows[i]["status"]).ToUpper()}\n\n");
            }
                        
            _telegram.SendMessage(finalMessageStringBuilder.ToString(), updateInfo.Message!.Chat.Id);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo)
            => updateInfo.Message!.Text!.IsCommand("/check");
    }
}