using System.Data;
using PrisonBot.Loop;

namespace PrisonBot.Functional
{
    public interface IInformationTableFactory
    {
        DataTable Create(IUpdateInfo updateInfo);
    }
}