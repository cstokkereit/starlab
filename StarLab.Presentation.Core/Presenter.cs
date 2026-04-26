using StarLab.Presentation.Configuration;
using Stratosoft.Commands;
using System.Diagnostics;

namespace StarLab.Presentation
{
    /// <summary>
    /// The base class for all presenters.
    /// </summary>
    public abstract class Presenter<TView> : Controller, IPresenter
    {
        private readonly ISessionContext context; // Provides access to the session context.

        private readonly ICommandManager commands; // Required for the creation and management of commands.

        private IApplicationController? controller; // A controller that creates, initialises and manages the views that comprise the user interface of the application.

        /// <summary>
        /// Initialises a new instance of the <see cref="Presenter{TView}"/> class.
        /// </summary>
        /// <param name="view">The <see cref="TView"/> controlled by the presenter.</param>
        /// <param name="context">An <see cref="ISessionContext"/> that provides access to the session context.</param>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of commands.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Presenter(TView view, ISessionContext context, ICommandManager commands, IEventAggregator events)
            : base(events)
        {
            this.commands = commands ?? throw new ArgumentNullException(nameof(commands));
            this.context = context ?? throw new ArgumentNullException(nameof(context));

            View = view ?? throw new ArgumentNullException(nameof(view));
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
        /// Gets the <see cref="ISessionContext"/> that provides access to the context for the current session.
        /// </summary>
        protected ISessionContext SessionContext => context;

        /// <summary>
        /// Returns true if the presenter has been initialised; false otherwise.
        /// </summary>
        protected bool Initialised => controller != null;

        /// <summary>
        /// Gets the <see cref="TView"/> that is controlled by the presenter.
        /// </summary>
        protected TView View { get; }

        /// <summary>
        /// Creates the specified command.
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
        /// Releases all resources used by the <see cref="DocumentViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Events.Unsubscribe(this);
            }
        }

        /// <summary>
        /// Gets the specified command.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <returns>The required <see cref="ICommand"/>.</returns>
        protected ICommand GetCommand(string name)
        {
            return commands.GetCommand(name);
        }

        /// <summary>
        /// Generates a command name by combining the specified name and target into a single string.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="target">The target associated with the command.</param>
        /// <returns>A command name in the form "name(target)".</returns>
        protected string GetCommandName(string name, string target)
        {
            return $"{name}({target})";
        }

        /// <summary>
        /// Sets the enabled state of the specified command.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="enabled">The new enabled state.</param>
        protected void UpdateCommandState(string action, bool enabled)
        {
            if (GetCommand(action) is IComponentCommand command) command.Enabled = enabled;
        }
    }
}
