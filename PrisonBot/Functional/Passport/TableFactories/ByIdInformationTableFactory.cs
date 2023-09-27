using System.Data;
using PrisonBot.Loop;
using PrisonBot.Tools;
using RelationalDatabasesViaOOP;

namespace PrisonBot.Functional
{
    public sealed class ByIdInformationTableFactory : IInformationTableFactory
    {
        private readonly IDatabase _database;

        public ByIdInformationTableFactory(IDatabase database) 
            => _database = database ?? throw new ArgumentNullException(nameof(database));

        public DataTable Create(IUpdateInfo updateInfo)
        {
            var screenedMessage = updateInfo.Message!.Text!.Replace("\'", "\\\'");
            var userId = long.Parse(screenedMessage.GetCommandArguments()[0]);
            var dataTable = _database.SendReadingRequest($"SELECT * FROM passports WHERE ids LIKE '%{userId}%'");

            if (dataTable.Rows.Count == 0)
                return dataTable;

            var tables = new List<DataTable> { dataTable, _database.SendReadingRequest($"SELECT * FROM statuses WHERE ids LIKE '%{userId}%'") };
            return tables.SmartMerge("ids");
        }
    }
}