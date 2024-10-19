using StarLab.Application.Events;

namespace StarLab.Application
{
    public abstract class Controller
    {
        private readonly IUseCaseFactory factory;

        private readonly IEventAggregator events;


        public Controller(IUseCaseFactory factory, IEventAggregator events)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            ArgumentNullException.ThrowIfNull(factory, nameof(events));

            this.factory = factory;
            this.events = events;
        }

        public abstract string Name { get; }

        protected IEventAggregator Events => events;

        protected IUseCaseFactory UseCaseFactory { get => factory; }
    }
}
