namespace StarLab.Presentation
{
    /// <summary>
    /// An abstract base class for implementations of the <see cref="IController"/> interface.
    /// </summary>
    public abstract class Controller : IController
    {
        private readonly IEventAggregator events; // This can be used for subscribing to and publishing events.

        /// <summary>
        /// Initialises a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="events">An <see cref="IEventAggregator"> that can be used for subscribing to and publishing events.</param>
        public Controller(IEventAggregator events)
        {
            this.events = events ?? throw new ArgumentNullException(nameof(events));
        }

        /// <summary>
        /// Gets the controller ID.
        /// </summary>
        public abstract string ID { get; }

        /// <summary>
        /// Releases all resources used by the presenter object.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Gets the <see cref="IEventAggregator"/> that can be used for subscribing to and publishing events.
        /// </summary>
        protected IEventAggregator Events => events;

        /// <summary>
        /// Releases all resources used by the presenter object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected abstract void Dispose(bool disposing);
    }
}
