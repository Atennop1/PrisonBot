using System.Data;
using PrisonBot.Loop;
using PrisonBot.Tools;

namespace PrisonBot.Functional
{
    public sealed class InformationTableFactory : IInformationTableFactory
    {
        private readonly IInformationTableFactory _withArgumentsTableFactory;
        private readonly IInformationTableFactory _withoutArgumentsTableFactory;

        public InformationTableFactory(IInformationTableFactory withArgumentsTableFactory, IInformationTableFactory withoutArgumentsTableFactory)
        {
            _withArgumentsTableFactory = withArgumentsTableFactory ?? throw new ArgumentNullException(nameof(withArgumentsTableFactory));
            _withoutArgumentsTableFactory = withoutArgumentsTableFactory ?? throw new ArgumentNullException(nameof(withoutArgumentsTableFactory));
        }

        public DataTable Create(IUpdateInfo updateInfo)
        {
            var arguments = updateInfo.Message!.Text!.GetCommandArguments();
            return arguments.Length == 0 ? _withoutArgumentsTableFactory.Create(updateInfo) : _withArgumentsTableFactory.Create(updateInfo);
        }
    }
}