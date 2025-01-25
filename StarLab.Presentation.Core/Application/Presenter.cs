using AutoMapper;
using StarLab.Commands;
using System.Diagnostics;

namespace StarLab.Application
{
    /// <summary>
    /// The base class for all presenters.
    /// </summary>
    public abstract class Presenter : Controller, IPresenter
    {
        private readonly Configuration.IConfigurationProvider configuration; // A service that provides the configuration information.

        private readonly ICommandManager commands; // Required for the creation and management of commands.

        private readonly IMapper mapper; // Copies data from model objects to data transfer objects and vice versa.

        private IApplicationController? controller; // A controller that creates, initialises and manages the views that comprise the user interface of the application.

        /// <summary>
        /// Initialises a new instance of the <see cref="Presenter"/> class.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of commands.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="Configuration.IConfigurationProvider"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Presenter(ICommandManager commands, IUseCaseFactory factory, Configuration.IConfigurationProvider configuration, IMapper mapper, IEventAggregator events)
            : base(factory, events)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.commands = commands ?? throw new ArgumentNullException(nameof(commands));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public virtual void Initialise(IApplicationController controller)
        {
            Debug.Assert(!Initialised);

            this.controller = controller ?? throw new ArgumentNullException(nameof(controller));

            AppController.RegisterCommandInvokers(commands);

            Events.Subsribe(this);
        }

        /// <summary>
        /// Gets the <see cref="IApplicationController"/> that manages the <see cref="IView"/>s.
        /// </summary>
        protected IApplicationController AppController
        {
            get
            {
                Debug.Assert(controller != null);
                return controller;
            }
        }

        /// <summary>
        /// Gets the <see cref="Configuration.IConfigurationProvider"/> that provides the configuration information.
        /// </summary>
        protected Configuration.IConfigurationProvider Configuration => configuration;

        /// <summary>
        /// Returns <see cref="true"/> if the presenter has been initialised; <see cref="false"/> otherwise.
        /// </summary>
        protected bool Initialised => controller != null;

        /// <summary>
        /// Gets the <see cref="IMapper"/> used to copy data from model objects to data transfer objects and vice versa.
        /// </summary>
        protected IMapper Mapper => mapper;

        /// <summary>
        /// Gets the specified <see cref="ICommand"/>. 
        /// </summary>
        /// <param name="controller">The <see cref="IController"/> that acts as the receiver for the command.</param>
        /// <param name="action">The action to be performed when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <param name="target">The target for the action.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to invoke the specified action.</returns>
        protected ICommand GetCommand(IController controller, string action, string target)
        {
            var name = action + target;

            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, AppController.CreateCommand(commands, controller, action, target));
            }

            return commands.GetCommand(name);
        }

        /// <summary>
        /// Gets the specified <see cref="ICommand"/>.
        /// </summary>
        /// <param name="controller">The <see cref="IController"/> that acts as the receiver for the command.</param>
        /// <param name="action">The action to be performed when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to invoke the specified action.</returns>
        protected ICommand GetCommand(IController controller, string action)
        {
            if (!commands.ContainsCommand(action))
            {
                commands.AddCommand(action, AppController.CreateCommand(commands, controller, action));
            }

            return commands.GetCommand(action);
        }

        /// <summary>
        /// Gets the specified <see cref="ICommand"/>.
        /// </summary>
        /// <param name="action">The action to be performed when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <param name="target">The target for the action.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to invoke the specified action.</returns>
        protected ICommand GetCommand(string action, string target)
        {
            var name = action + target;

            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, AppController.CreateCommand(commands, this, action, target));
            }

            return commands.GetCommand(name);
        }

        /// <summary>
        /// Gets the specified <see cref="ICommand"/>.
        /// </summary>
        /// <param name="action">The action to be performed when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to invoke the specified action.</returns>
        protected ICommand GetCommand(string action)
        {
            if (!commands.ContainsCommand(action))
            {
                commands.AddCommand(action, AppController.CreateCommand(commands, this, action));
            }

            return commands.GetCommand(action);
        }

        /// <summary>
        /// Gets the specified <see cref="ICommandChain"/> that can be used to execute multiple <see cref="ICommand"/>s in sequence.
        /// </summary>
        /// <param name="name">The name of the required <see cref="ICommandChain"/>.</param>
        /// <returns>The specified instance of <see cref="ICommandChain"/>.</returns>
        protected ICommandChain GetCommandChain(string name)
        {
            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, new CommandChain(commands));
            }

            return (ICommandChain)commands.GetCommand(name);
        }

        /// <summary>
        /// Gets an <see cref="ICommandChain"/> that can be used to execute multiple <see cref="ICommand"/>s in sequence.
        /// </summary>
        /// <returns>An instance of <see cref="ICommandChain"/>.</returns>
        protected ICommandChain GetCommandChain()
        {
            return new CommandChain(commands);
        }

        /// <summary>
        /// Gets the specified <see cref="ICommand"/>.
        /// </summary>
        /// <param name="view">The name of the <see cref="IView"/> that will be shown when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to show the specified <see cref="IView"/>.</returns>
        protected ICommand GetShowViewCommand(string view)
        {
            var name = Actions.SHOW + view;

            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, AppController.CreateCommand(commands, view));
            }

            return commands.GetCommand(name);
        }

        /// <summary>
        /// Sets the enabled state of the specified <see cref="ICommand"/>.
        /// </summary>
        /// <param name="action">The action to be performed when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <param name="target">The target for the action.</param>
        /// <param name="enabled">The new enabled state.</param>
        protected void UpdateCommandState(string action, string target, bool enabled)
        {
            if (GetCommand(action + target) is IComponentCommand command) command.Enabled = enabled;
        }

        /// <summary>
        /// Sets the enabled state of the specified <see cref="ICommand"/>.
        /// </summary>
        /// <param name="action">The action to be performed when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <param name="enabled">The new enabled state.</param>
        protected void UpdateCommandState(string action, bool enabled)
        {
            if (GetCommand(action) is IComponentCommand command) command.Enabled = enabled;
        }
    }
}
