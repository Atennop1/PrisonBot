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

            var loopObjects = new List<ILoopObject>
            {
                new HelloBot(telegram),
                new ShowPassportBot(database, telegram)
            };

            var updatingCycle = new UpdatingLoop(client, loopObjects);
            updatingCycle.Activate();

        }
    }
}