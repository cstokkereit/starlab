using log4net;
using StarLab.Application;
using StarLab.Application.Configuration;

namespace StarLab
{
    /// <summary>
    /// A <see cref="Form"/> that implements the behaviour that is common to all dialogs.
    /// </summary>
    public partial class DialogView : Form, IDialog, IDialogView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DialogView)); // The logger that will be used for writing log messages.

        private readonly IDialogViewPresenter presenter; // The presenter that controls the view.

        private readonly IChildView childView; // A view that implements the dialog specific behaviour.

        private readonly string id; // The view ID.

        /// <summary>
        /// Initialises a new instance of the <see cref="DialogView"> class.
        /// </summary>
        /// <param name="name">The name of the dialog.</param>
        /// <param name="text">The dialog text.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the presenter and child view.</param>
        /// <param name="configuration">An <see cref="IViewConfiguration"/> that holds the configuration information required to construct this view.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public DialogView(string name, string text, IViewFactory factory, IViewConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(text, nameof(text));

            if (configuration.ChildViews.Count > 1) throw new ArgumentException(); // TODO

            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;

            Name = name;
            Text = text;
            id = name;

            presenter = (IDialogViewPresenter)factory.CreatePresenter(configuration.Name, this);

            childView = factory.CreateView(configuration.ChildViews[0], configuration);
            childView.Controller.RegisterController((IViewController)presenter);

            SuspendLayout();

            if (childView is Control control)
            {
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }

            ResumeLayout();
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            childView.Controller.Initialise(controller);
        }

        /// <summary>
        /// Shows the dialog with the specified <see cref="IInteractionContext"/>.
        /// </summary>
        /// <param name="context">An <see cref="IInteractionContext"/> that provides the context required to configure the dialog for a specific user interaction.</param>
        public void Show(IInteractionContext context)
        {
            childView.Controller.Run(context);
        }

        /// <summary>
        /// Gets the <see cref="IViewController"> that controls this view.
        /// </summary>
        public IViewController Controller => (IViewController)presenter;

        /// <summary>
        /// Gets or sets a flag that determines whether the dialog box will be hidden or unloaded when it is closed.
        /// </summary>
        public bool HideOnClose { get; set; }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public string ID => id;

        /// <summary>
        /// Shows the specified view.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            if (view is Form form) form.ShowDialog(this);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the button that was clicked.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
            return DialogController.ShowMessage(this, caption, message, type, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionResponses responses)
        {
            return DialogController.ShowMessage(this, caption, message, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message)
        {
            return DialogController.ShowMessage(this, caption, message);
        }

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            return DialogController.ShowOpenFileDialog(this, title, filter);
        }

        /// <summary>
        /// Displays a <see cref="SaveFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return DialogController.ShowSaveFileDialog(this, title, filter, extension);
        }

        /// <summary>
        /// Event handler for the <see cref="Form.FormClosing"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">A <see cref="FormClosingEventArgs"/> that provides context for the event.</param>
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (HideOnClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
