namespace Stratosoft.Commands
{
    /// <summary>
    /// An interface used to mock the receiver used by classes implementing the <see cref="Command{TReceiver}"/> interface.
    /// </summary>
    public interface IReceiver
    {
        void Test();
    }

    /// <summary>
    /// An interface used to mock the receiver used by classes implementing the <see cref="Command{TReceiver}"/> interface.
    /// </summary>
    public interface IReceiver<Arguments>
    {
        void Test(Arguments arguments);
    }
}
