namespace StarLab.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReceiver
    {
        void Test();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IReceiver<Arguments>
    {
        void Test(Arguments arguments);
    }
}
