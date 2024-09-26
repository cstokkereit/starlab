namespace StarLab.Application.Events
{
    /// <summary>
    /// An interface to be implemented by subscribers of the specified event type.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    public interface ISubscriber<T>
    {
        void OnEvent(T e);
    }
}
