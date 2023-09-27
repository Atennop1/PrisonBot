using System.Data;
using PrisonBot.Loop;
using PrisonBot.Tools;
using RelationalDatabasesViaOOP;

namespace PrisonBot.Functional
{
    public sealed class ByNicknameInformationTableFactory : IInformationTableFactory
    {
        private readonly IDatabase _database;

        public ByNicknameInformationTableFactory(IDatabase database) 
            => _database = database ?? throw new ArgumentNullException(nameof(database));
        
        public DataTable Create(IUpdateInfo updateInfo)
        {
            var screenedMessage = updateInfo.Message!.Text!.Replace("\'","\\\'");
            var nickname = string.Join(' ', screenedMessage.GetCommandArguments());
            var dataTable = _database.SendReadingRequest($"SELECT * FROM passports WHERE UPPER(nickname) = UPPER('{nickname}')");
            
            if (dataTable.Rows.Count == 0)
                return dataTable;
                
            var tables = new List<DataTable> { dataTable, _database.SendReadingRequest($"SELECT * FROM statuses WHERE ids LIKE '%{dataTable.Rows[0]["ids"]}%'") };
            return tables.SmartMerge("ids");
        }
    }
}