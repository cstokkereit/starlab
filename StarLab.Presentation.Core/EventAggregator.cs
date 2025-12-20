namespace StarLab.Presentation
{
    /// <summary>
    /// A class used for subscribing to and publishing application level events.
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, List<WeakReference>> subsribers = new Dictionary<Type, List<WeakReference>>(); // A dictionary containing a list of subscribers for each event.

        private readonly object lockSubscriberDictionary = new object(); // An object used to lock the subscribers dictionary.

        /// <summary>
        /// Publishes an event.
        /// </summary>
        /// <typeparam name="TEventType">The event type.</typeparam>
        /// <param name="payload">The event to publish.</param>
        /// <param name="synchronous">If true the event will be published synchronously.</param>
        public void Publish<TEventType>(TEventType payload, bool synchronous = false)
        {
            var type = typeof(ISubscriber<>).MakeGenericType(typeof(TEventType));

            var subscribers = GetSubscriberList(type);

            if (subscribers != null)
            {
                lock (lockSubscriberDictionary)
                {
                    List<WeakReference> subsribersToBeRemoved = new List<WeakReference>();

                    foreach (var subsriber in subscribers)
                    {
                        if (subsriber.IsAlive)
                        {
                            if (subsriber.Target is ISubscriber<TEventType> subscriber) InvokeSubscribedEvent(payload, subscriber, synchronous);
                        }
                        else
                        {
                            subsribersToBeRemoved.Add(subsriber);
                        }
                    }

                    if (subsribersToBeRemoved.Any())
                    {
                        foreach (var remove in subsribersToBeRemoved)
                        {
                            subscribers.Remove(remove);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Subscribe to the event determined by the value of the type parameter specified in the implementation.
        /// </summary>
        /// <param name="subscriber">An object that implements the <see cref="ISubscriber{TEventType}"/> interface.</param>
        public void Subsribe(object subscriber)
        {
            lock (lockSubscriberDictionary)
            {
                var types = subscriber.GetType().GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));

                var reference = new WeakReference(subscriber);

                foreach (var type in types)
                {
                    GetSubscriberList(type)?.Add(reference);
                }
            }
        }

        /// <summary>
        /// Gets a list containing the subscribers to the specified event type.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> of event for which the list of subscribers is required.</param>
        /// <returns>A <see cref="List{WeakReference}"/> containing the subscribers to the type of event specified.</returns>
        private List<WeakReference>? GetSubscriberList(Type type)
        {
            List<WeakReference>? references = null;

            lock (lockSubscriberDictionary)
            {
                bool found = subsribers.TryGetValue(type, out references);

                if (!found)
                {
                    references = new List<WeakReference>();
                    subsribers.Add(type, references);
                }
            }

            return references;
        }

        /// <summary>
        /// Invokes the <see cref="OnEvent"/> method of the specified subscriber asynchronously.
        /// </summary>
        /// <typeparam name="TEventType">The event type.</typeparam>
        /// <param name="payload">The event being invoked.</param>
        /// <param name="subscriber">The <see cref="ISubscriber{TEventType}"/> receiving the event.</param>
        /// <param name="synchronous">If true the event will be published synchronously.</param>
        private void InvokeSubscribedEvent<TEventType>(TEventType payload, ISubscriber<TEventType> subscriber, bool synchronous)
        {
            var context = SynchronizationContext.Current ?? new SynchronizationContext();

            if (synchronous)
            {
                context.Send(s => subscriber.OnEvent(payload), null);
            }
            else
            {
                context.Post(s => subscriber.OnEvent(payload), null);
            }
        }
    }
}
