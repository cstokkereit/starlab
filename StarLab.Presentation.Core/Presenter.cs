using Stratosoft.Commands;
using System.Diagnostics;

namespace StarLab.Presentation
{
    /// <summary>
    /// The base class for all presenters.
    /// </summary>
    public abstract class Presenter<TView> : Controller, IPresenter
    {
        private readonly IApplicationSettings settings; // Provides access to the application configuration.

        private readonly ICommandManager commands; // Required for the creation and management of commands.

        private IApplicationController? controller; // A controller that creates, initialises and manages the views that comprise the user interface of the application.

        /// <summary>
        /// Initialises a new instance of the <see cref="Presenter{TView}"/> class.
        /// </summary>
        /// <param name="view">The <see cref="TView"/> controlled by the presenter.</param>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of commands.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Presenter(TView view, ICommandManager commands, IApplicationSettings settings, IEventAggregator events)
            : base(events)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.commands = commands ?? throw new ArgumentNullException(nameof(commands));

            View = view;
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public virtual void Initialise(IApplicationController controller)
        {
            Debug.Assert(!Initialised); // TODO throw an error?

            this.controller = controller ?? throw new ArgumentNullException(nameof(controller));

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
        /// Gets the <see cref="IApplicationSettings"/> that provides the configuration information.
        /// </summary>
        protected IApplicationSettings Settings => settings;

        /// <summary>
        /// Returns true if the presenter has been initialised; false otherwise.
        /// </summary>
        protected bool Initialised => controller != null;

        /// <summary>
        /// Gets the <see cref="TView"/> that is controlled by the presenter.
        /// </summary>
        protected TView View { get; }

        /// <summary>
        /// Creates the specified <see cref="ICommand"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        protected ICommand CreateCommand(string name, Action action)
        {
            if (!commands.ContainsCommand(name)) //throw new InvalidOperationException(string.Format(Resources.CommandAlreadyExists, name));
            {
                commands.AddCommand(name, new ComponentCommand(commands, action));
            }
            
            return commands.GetCommand(name);
        }

        /// <summary>
        /// Gets the specified command."/>
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <returns>The required <see cref="ICommand"/>.</returns>
        protected ICommand GetCommand(string name)
        {
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
