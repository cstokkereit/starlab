using log4net;
using StarLab.Presentation;
using StarLab.Shared.Properties;
using StarLab.UI;
using System.Diagnostics;

namespace StarLab
{
    /// <summary>
    /// A <see cref="Form"/> that implements the behaviour that is common to all dialogs.
    /// </summary>
    public partial class DialogView : Form, IDialog, IDialogView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DialogView)); // The logger that will be used for writing log messages.

        private readonly string id; // The view ID.

        private IChildView? childView; // A view that implements the dialog specific behaviour.

        private IDialogViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="DialogView"> class.
        /// </summary>
        /// <param name="name">The name of the dialog.</param>
        /// <param name="text">The dialog text.</param>
        /// <param name="definition">The <see cref="IViewDefinition"/> used to construct this view.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public DialogView(string name, string text, IViewDefinition definition)
        {
            ArgumentNullException.ThrowIfNull(definition, nameof(definition));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(text, nameof(text));

            Debug.Assert(definition.ChildViewDefinitions.Count == 1);

            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;

            Name = name;
            Text = text;
            id = name;

            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.InstanceCreated, nameof(DialogView)));
        }

        /// <summary>
        /// Gets the <see cref="IViewController"> that controls this view.
        /// </summary>
        public IViewController? Controller => (IViewController?)presenter;

        /// <summary>
        /// Gets or sets a flag that determines whether the dialog box will be hidden or unloaded when it is closed.
        /// </summary>
        public bool HideOnClose { get; set; }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public string ID => id;

        /// <summary>
        /// Attaches the <see cref="IPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IPresenter"/> that controls the view.</param>
        public void Attach(IPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(); // TODO

            this.presenter = (IDialogViewPresenter)presenter;
        }

        /// <summary>
        /// Attaches the <see cref="IChildView"/> to the view.
        /// </summary>
        /// <param name="childView">The <see cref="IChildView"/> to attach.</param>
        public void Attach(IChildView childView)
        {
            ArgumentNullException.ThrowIfNull(childView, nameof(childView));
            
            Debug.Assert(childView.Controller != null);

            if (presenter is IViewController controller)
            {
                childView.Controller.RegisterController(controller);
            }
            
            SuspendLayout();

            if (childView is Control control)
            {
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }

            ResumeLayout();

            this.childView = childView;

            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.ViewAttached, childView.Name, nameof(DialogView)));
        }

        /// <summary>
        /// Detaches the <see cref="IPresenter"/> that controls the view.
        /// </summary>
        public void Detach()
        {
            presenter = null;
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            Debug.Assert(childView != null);
            Debug.Assert(childView.Controller != null);

            childView.Controller.Initialise(controller);
        }

        /// <summary>
        /// Shows the specified view.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            if (view is Form form) form.ShowDialog(this);
        }

        /// <summary>
        /// Shows the dialog with the specified <see cref="IInteractionContext"/>.
        /// </summary>
        /// <param name="context">An <see cref="IInteractionContext"/> that provides the context required to configure the dialog for a specific user interaction.</param>
        public void Show(IInteractionContext context)
        {
            Debug.Assert(childView != null);
            Debug.Assert(childView.Controller != null);

            childView.Controller.Run(context);
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
