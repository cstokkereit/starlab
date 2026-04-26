using log4net;
using StarLab.Application;
using StarLab.Presentation.Configuration;
using StarLab.Shared;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IDockableView"/>.
    /// </summary>
    public class ToolViewPresenter : Presenter<IDockableView>, IDockableViewPresenter, IViewController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ToolViewPresenter)); // The logger that will be used for writing log messages.

        private readonly IChildViewController childController;

        /// <summary>
        /// Initialises a new instance of the <see cref="ToolViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IDockableView"/> controlled by this presenter.</param>
        /// <param name="childController">The <see cref="IChildViewController"/> that controls the child view.</param>
        /// <param name="context">An <see cref="ISessionContext"/> that provides access to the session context.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="settings">An <see cref="IUserSettings"/> that provides access to the application configuration.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ToolViewPresenter(IDockableView view, IChildViewController childController, ISessionContext context, ICommandManager commands, IEventAggregator events)
            : base(view, context, commands, events)
        {
            this.childController = childController ?? throw new ArgumentNullException(nameof(childController));

            ID = Controllers.GetControllerID(view);

            View.Attach(this);

            Location = Constants.DockRight; // TODO - Optional default locations?
        }

		/// <summary>
		/// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
		/// </summary>
		~ToolViewPresenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IChildViewController}"/> containing the child controllers.
        /// </summary>
        public IEnumerable<IChildViewController> ChildControllers => [childController];

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string ID { get; }

        /// <summary>
        /// Gets or sets the current location of the view.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Releases all resources used by the <see cref="ToolViewPresenter"/> object.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (Initialised) throw new InvalidOperationException(string.Format(Resources.AlreadyInitialised, nameof(ToolViewPresenter)));

            base.Initialise(controller);

            childController.Initialise(controller);

            log.Debug(string.Format(LogEntries.Initialised, $"{nameof(ToolViewPresenter)}({View.Name})"));
        }

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            View.Show(view);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified caption, message, message type and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
            return View.ShowMessage(caption, message, type, responses);
        }

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified owner and options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            return View.ShowOpenFileDialog(title, filter);
        }

        /// <summary>
        /// Displays a save file dialog with the specified owner and options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return View.ShowSaveFileDialog(title, filter, extension);
        }

        /// <summary>
        /// Notifies the presenter that the view has been activated.
        /// </summary>
        public void ViewActivated()
        {
            Events.Publish(new ActiveViewChangedEventArgs(View));
        }

        /// <summary>
        /// Releases all resources used by the <see cref="ToolViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                View.Detach();
            }
        }
    }
}
