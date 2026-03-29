using log4net;
using StarLab.Application;
using StarLab.Shared.Properties;
using StarLab.Shared.Resources;
using Stratosoft.Commands;
using System.ComponentModel;

namespace StarLab.Presentation
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IDialogView"/>.
    /// </summary>
    public class DialogViewPresenter : Presenter<IDialogView>, IDialogViewPresenter, IViewController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DialogViewPresenter)); // The logger that will be used for writing log messages.

        private readonly IChildViewController childController;

        /// <summary>
        /// Initialises a new instance of the <see cref="DialogViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IDialogView"/> controlled by this presenter.</param>
        /// <param name="childController">The <see cref="IChildViewController"/> that controls the child view.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public DialogViewPresenter(IDialogView view, IChildViewController childController, ICommandManager commands, IApplicationSettings settings, IEventAggregator events)
            : base(view, commands, settings, events)
        {
            this.childController = childController;

            ID = Controllers.GetControllerID(view);

            View.Attach(this);
        }

		/// <summary>
		/// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
		/// </summary>
		~DialogViewPresenter()
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
        /// Closes the dialog box.
        /// </summary>
        public void Close()
        {
            View.Close();
        }

        /// <summary>
        /// Releases all resources used by the <see cref="DialogViewPresenter"/> object.
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
            if (Initialised) throw new InvalidOperationException(string.Format(Resources.AlreadyInitialised, nameof(DialogViewPresenter)));

            base.Initialise(controller);

            childController.Initialise(controller);

            log.Debug(string.Format(LogEntries.Initialised, $"{nameof(DialogViewPresenter)}({View.Name})"));
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
        /// Notifies the presenter that the view is being closed.
        /// </summary>
        /// <param name="e">The <see cref="CancelEventArgs"/> that can be used to determine the reasons that the view is closing and, if necessary, cancel it.</param>
        public void ViewClosing(CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Releases all resources used by the <see cref="DialogViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                View.Detach();
            }
        }
    }
}
