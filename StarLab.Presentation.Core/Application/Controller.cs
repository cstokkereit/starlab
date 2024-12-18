namespace StarLab.Application
{
    /// <summary>
    /// An abstract base class for implementations of the <see cref="IController"/> interface.
    /// </summary>
    public abstract class Controller : IController
    {
        private readonly IEventAggregator events; // This can be used for subscribing to and publishing events.

        private readonly IUseCaseFactory factory; // This can be used to create use case interactors.

        /// <summary>
        /// Initialises a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="factory">An <see cref="IUseCaseFactory"> that can be used to create instances of the interactors that implement the use cases.</param>
        /// <param name="events">An <see cref="IEventAggregator"> that can be used for subscribing to and publishing events.</param>
        public Controller(IUseCaseFactory factory, IEventAggregator events)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            ArgumentNullException.ThrowIfNull(factory, nameof(events));

            this.factory = factory;
            this.events = events;
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the <see cref="IEventAggregator"/> that can be used for subscribing to and publishing events.
        /// </summary>
        protected IEventAggregator Events => events;

        /// <summary>
        /// Gets the <see cref="IUseCaseFactory"> that can be used to create instances of the interactors that implement the use cases.
        /// </summary>
        protected IUseCaseFactory UseCaseFactory => factory;
    }
}
