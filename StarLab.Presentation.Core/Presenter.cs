using AutoMapper;
using StarLab.Application.UseCases;
using StarLab.Commands;
using StarLab.Presentation.Events;

namespace StarLab.Presentation
{
    /// <summary>
    /// The base class for all presenters.
    /// </summary>
    public abstract class Presenter : Controller, IPresenter
    {
        private readonly IDictionary<string, IComponentCommand> commands = new Dictionary<string, IComponentCommand>();

        private readonly IConfiguration configuration;

        private readonly IEventAggregator events;

        private readonly IMapper mapper;

        public Presenter(IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(useCaseFactory)
        {
            this.configuration = configuration;
            this.events = events;
            this.mapper = mapper;
        }

        #region IPresenter Members

        /// <summary>
        /// 
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Initialises the presenter.
        /// </summary>
        /// <param name="controller">The application controller.</param>
        public virtual void Initialise(IApplicationController controller)
        {
            // Do Nothing
        }

        #endregion

        protected IConfiguration Configuration => configuration;

        protected IEventAggregator Events => events;

        protected IMapper Mapper => mapper;

        protected static ControllerAction<T> CreateAction<T>(T controller, string verb, string target) where T : IController
        {
            return new ControllerAction<T>(controller, verb, target);
        }

        protected static ControllerAction<T> CreateAction<T>(T controller, string verb) where T : IController
        {
            return new ControllerAction<T>(controller, verb);
        }

        protected IComponentCommand GetCommand(string name)
        {
            return commands[name];
        }

        protected void SaveCommand(string name, IComponentCommand command)
        {
            commands.Add(name, command);
        }
    }
}
