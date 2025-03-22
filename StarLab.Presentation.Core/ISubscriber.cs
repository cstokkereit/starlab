namespace StarLab.Presentation
{
    /// <summary>
    /// An interface to be implemented by subscribers of the specified event type. TODO
    /// </summary>
    /// <typeparam name="TEventType">The event type.</typeparam>
    public interface ISubscriber<TEventType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        void OnEvent(TEventType e);
    }
}
