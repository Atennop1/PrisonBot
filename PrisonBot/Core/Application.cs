using PrisonBot.Functional;
using PrisonBot.Loop;

namespace PrisonBot.Core
{
    public sealed class Application
    {
        public void Run()
        {
            var clientFactory = new TelegramClientFactory();
            var client = clientFactory.Create();
            var telegram = new Telegram.Telegram(client);

            var databaseFactory = new RelationalDatabaseFactory();
            var database = databaseFactory.Create();

            var withoutArgumentsTableFactory = new WithoutArgumentsInformationTableFactory(database);
            var byIdTableFactory = new ByIdInformationTableFactory(database);
            var byNicknameTableFactory = new ByNicknameInformationTableFactory(database);
            
            var withArgumentsTableFactory = new WithArgumentsInformationTableFactory(byIdTableFactory, byNicknameTableFactory);
            var informationTableFactory = new InformationTableFactory(withArgumentsTableFactory, withoutArgumentsTableFactory);

            var loopObjects = new List<ILoopObject>
            {
                new HelloBot(telegram),
                new DisplayPassportBot(telegram, informationTableFactory)
            };

            var updatingCycle = new UpdatingLoop(client, loopObjects);
            updatingCycle.Activate();
        }
    }
}