using System.Data;
using PrisonBot.Loop;
using PrisonBot.Tools;
using RelationalDatabasesViaOOP;

namespace PrisonBot.Functional
{
    public sealed class WithoutArgumentsInformationTableFactory : IInformationTableFactory
    {
        private readonly IDatabase _database;

        public WithoutArgumentsInformationTableFactory(IDatabase database) 
            => _database = database ?? throw new ArgumentNullException(nameof(database));

        public DataTable Create(IUpdateInfo updateInfo)
        {
            var userId = updateInfo.Message!.ReplyToMessage == null ? updateInfo.Message!.From!.Id : updateInfo.Message!.ReplyToMessage!.From!.Id;
            var dataTable = _database.SendReadingRequest($"SELECT * FROM passports WHERE ids LIKE '%{userId}%'");
            
            var tables = new List<DataTable> { dataTable, _database.SendReadingRequest($"SELECT * FROM statuses WHERE ids LIKE '%{userId}%'") };
            return tables.SmartMerge("ids");
        }
    }
}