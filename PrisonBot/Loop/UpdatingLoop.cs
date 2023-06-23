using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace PrisonBot.Loop
{
    public sealed class UpdatingLoop : IUpdatingLoop
    {
        private readonly BotClient _client;
        private readonly List<ILoopObject> _loopObjects;

        public UpdatingLoop(BotClient client, List<ILoopObject> loopObjects)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _loopObjects = loopObjects ?? throw new ArgumentNullException(nameof(loopObjects));
        }

        public void Activate()
        {
            var updates = _client.GetUpdates();

            while (true)
            {
                if (!updates.Any())
                {
                    updates = _client.GetUpdates();
                    continue;
                }
                
                foreach (var update in updates)
                {
                    var updateInfo = new LibraryUpdateInfoAdapter(update);

                    foreach (var loopObject in _loopObjects)
                    {
                        if (!loopObject.RequiredTypeOfUpdate.HasFlag(updateInfo.Type) || !loopObject.CanGetUpdate(updateInfo)) 
                            continue;
                        
                        loopObject.GetUpdate(updateInfo);
                        break;
                    }
                }

                var offset = updates.Last().UpdateId + 1;
                updates = _client.GetUpdates(offset, limit: 10, timeout: 60);
            }
        }
    }
}