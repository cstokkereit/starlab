using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using StarLab.Shared.Properties;
using System.Diagnostics;

namespace StarLab.Application
{
    /// <summary>
    /// The base class for all presenters.
    /// </summary>
    public abstract class Presenter : Controller, IPresenter
    {
        private readonly IConfigurationService configuration;

        private readonly ICommandManager commands;

        private readonly IMapper mapper;

        private IApplicationController? controller;

        public Presenter(ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(useCaseFactory, events)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.commands = commands ?? throw new ArgumentNullException(nameof(commands));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Initialises the presenter.
        /// </summary>
        /// <param name="controller">The application controller.</param>
        public virtual void Initialise(IApplicationController controller)
        {
            Debug.Assert(!Initialised);

            this.controller = controller ?? throw new ArgumentNullException(nameof(controller));

            AppController.RegisterCommandInvokers(commands);

            Events.Subsribe(this);
        }

        protected IApplicationController AppController
        {
            get
            {
                Debug.Assert(controller != null);
                return controller;
            }
        }

        protected IConfigurationService Configuration => configuration;

        protected bool Initialised => controller != null;

        protected IMapper Mapper => mapper;

        protected ICommand GetCommand(IController controller, string action, string target)
        {
            var name = action + target;

            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, AppController.CreateCommand(commands, controller, action, target));
            }

            return commands.GetCommand(name);
        }

        protected ICommand GetCommand(string action, string target)
        {
            var name = action + target;

            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, AppController.CreateCommand(commands, this, action, target));
            }

            return commands.GetCommand(name);
        }

        protected ICommand GetCommand(IController controller, string action)
        {
            if (!commands.ContainsCommand(action))
            {
                commands.AddCommand(action, AppController.CreateCommand(commands, controller, action));
            }

            return commands.GetCommand(action);
        }

        protected ICommand GetCommand(string action)
        {
            if (!commands.ContainsCommand(action))
            {
                commands.AddCommand(action, AppController.CreateCommand(commands, this, action));
            }

            return commands.GetCommand(action);
        }

        protected ICommandChain GetCommandChain(string name)
        {
            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, new CommandChain(commands));
            }

            return (ICommandChain)commands.GetCommand(name);
        }

        protected ICommandChain GetCommandChain()
        {
            return new CommandChain(commands);
        }

        protected ICommand GetShowViewCommand(string view)
        {
            var name = Actions.SHOW + view;

            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, AppController.CreateCommand(commands, view));
            }

            return commands.GetCommand(name);
        }

        protected void UpdateCommandState(string action, string target, bool enabled)
        {
            if (GetCommand(action + target) is IComponentCommand command) command.Enabled = enabled;
        }

        protected void UpdateCommandState(string action, bool enabled)
        {
            if (GetCommand(action) is IComponentCommand command) command.Enabled = enabled;
        }
    }
}
