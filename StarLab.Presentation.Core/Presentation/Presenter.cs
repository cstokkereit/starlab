using AutoMapper;
using StarLab.Commands;
using StarLab.Application.Events;
using StarLab.Application;
using StarLab.Shared.Properties;

namespace StarLab.Presentation
{
    /// <summary>
    /// The base class for all presenters.
    /// </summary>
    public abstract class Presenter : Controller, IPresenter
    {
        private readonly IDictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        private readonly IConfiguration configuration;

        private readonly IEventAggregator events;

        private readonly IMapper mapper;

        private IApplicationController? controller;

        public Presenter(IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(useCaseFactory)
        {
            this.configuration = configuration;
            this.events = events;
            this.mapper = mapper;
        }

        #region IPresenter Members

        /// <summary>
        /// Initialises the presenter.
        /// </summary>
        /// <param name="controller">The application controller.</param>
        public virtual void Initialise(IApplicationController controller)
        {
            this.controller = controller;
        }

        #endregion

        protected IApplicationController AppController 
        { 
            get
            {
                if (controller == null) throw new InvalidOperationException(Resources.MessageNotInitialised);

                return controller;
            }
        }

        protected IConfiguration Configuration => configuration;

        protected IEventAggregator Events => events;

        protected IMapper Mapper => mapper;

        protected ICommand GetCommand(string name)
        {
            return commands[name];
        }

        protected void SaveCommand(string name, ICommand command)
        {
            commands.Add(name, command);
        }
    }
}
