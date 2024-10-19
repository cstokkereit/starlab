namespace StarLab.Application.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, List<WeakReference>> subsribers = new Dictionary<Type, List<WeakReference>>();

        private readonly object lockSubscriberDictionary = new object();

        /// <summary>
        /// Publish an event.
        /// </summary>
        /// <typeparam name="TEventType"></typeparam>
        /// <param name="eventToPublish"></param>
        public void Publish<T>(T eventToPublish)
        {
            var type = typeof(ISubscriber<>).MakeGenericType(typeof(T));

            var subscribers = GetSubscriberList(type);

            lock (lockSubscriberDictionary)
            {
                List<WeakReference> subsribersToBeRemoved = new List<WeakReference>();

                foreach (var subsriber in subscribers)
                {
                    if (subsriber.IsAlive)
                    {
                        var subscriber = subsriber.Target as ISubscriber<T>;

                        if (subscriber != null)
                        {
                            InvokeSubscriberEvent(eventToPublish, subscriber);
                        }
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

        /// <summary>
        /// Subribe to an event.
        /// </summary>
        /// <param name="subscriber"></param>
        public void Subsribe(object subscriber)
        {
            lock (lockSubscriberDictionary)
            {
                var types = subscriber.GetType().GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));

                var reference = new WeakReference(subscriber);

                foreach (var type in types)
                {
                    var subscribers = GetSubscriberList(type);
                    subscribers.Add(reference);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <param name="subscriber"></param>
        private void InvokeSubscriberEvent<T>(T e, ISubscriber<T> subscriber)
        {
            //Synchronize the invocation of method 

            var context = SynchronizationContext.Current;

            if (context == null)
            {
                context = new SynchronizationContext();
            }

            context.Post(s => subscriber.OnEvent(e), null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<WeakReference> GetSubscriberList(Type type)
        {
            List<WeakReference> references = null;

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
    }
}
