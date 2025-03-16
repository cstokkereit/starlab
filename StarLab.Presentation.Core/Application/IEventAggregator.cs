namespace StarLab.Application
{
    /// <summary>
    /// Represents an event agrregator that can be used to decouple the publishers of events from their subscribers.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Publishes an event.
        /// </summary>
        /// <typeparam name="TEventType">The event type.</typeparam>
        /// <param name="payload">The event to publish.</param>
        /// <param name="synchronous">If true the event will be published synchronously.</param>
        void Publish<TEventType>(TEventType payload, bool synchronous = false);

        /// <summary>
        /// Subscribe to the event determined by the value of the type parameter specified in the implementation.
        /// </summary>
        /// <param name="subscriber">An object that implements the <see cref="ISubscriber{TEventType}"/> interface.</param>
        void Subsribe(object subscriber);
    }
}
