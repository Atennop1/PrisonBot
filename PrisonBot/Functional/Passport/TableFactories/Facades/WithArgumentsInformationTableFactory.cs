using System.Data;
using PrisonBot.Loop;
using PrisonBot.Tools;

namespace PrisonBot.Functional
{
    public sealed class WithArgumentsInformationTableFactory : IInformationTableFactory
    {
        private readonly IInformationTableFactory _byIdTableFactory;
        private readonly IInformationTableFactory _byNicknameTableFactory;

        public WithArgumentsInformationTableFactory(IInformationTableFactory byIdTableFactory, IInformationTableFactory byNicknameTableFactory)
        {
            _byIdTableFactory = byIdTableFactory ?? throw new ArgumentNullException(nameof(byIdTableFactory));
            _byNicknameTableFactory = byNicknameTableFactory ?? throw new ArgumentNullException(nameof(byNicknameTableFactory));
        }

        public DataTable Create(IUpdateInfo updateInfo)
        {
            var argumentsArray = updateInfo.Message!.Text!.GetCommandArguments();
            return long.TryParse(argumentsArray[0], out _) ? _byIdTableFactory.Create(updateInfo) : _byNicknameTableFactory.Create(updateInfo);
        }
    }
}