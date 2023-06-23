namespace PrisonBot.Loop
{
    public interface ILoopObject
    {
        TypeOfUpdate RequiredTypeOfUpdate { get; }
        
        void GetUpdate(IUpdateInfo updateInfo);
        bool CanGetUpdate(IUpdateInfo updateInfo);
    }
}