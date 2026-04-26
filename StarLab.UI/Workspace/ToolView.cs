using log4net;
using StarLab.Application;
using StarLab.Presentation;
using StarLab.Presentation.Workspace;
using StarLab.Shared;
using StarLab.Shared.Properties;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.UI.Workspace
{
    /// <summary>
    /// A <see cref="DockContent"/> that implements the behaviour that is common to all dockable tool windows.
    /// </summary>
    public sealed partial class ToolView : DockContent, IDockableView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ToolView)); // The logger that will be used for writing log messages.

        private IChildView childView; // A view that implements the tool specific behaviour.

        private IDockableViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="ToolView"> class.
        /// </summary>
        /// <param name="name">The name of the tool window.</param>
        /// <param name="text">The tool window text.</param>
        /// <param name="childView">The <see cref="IChildView"/> used to construct this view.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public ToolView(string name, string text, IChildView childView)
        {
            ArgumentNullException.ThrowIfNull(childView, nameof(childView));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(text, nameof(text));

            InitializeComponent();

            SuspendLayout();

            if (childView is Control control)
            {
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }

            ResumeLayout();

            this.childView = childView;

            Name = name;
            Text = text;
            ID = name;
        }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// Attaches the <see cref="IPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IPresenter"/> that controls the view.</param>
        public void Attach(IPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(Resources.PresenterAlreadyAttached);

            this.presenter = (IDockableViewPresenter)presenter;

            log.Debug(string.Format(LogEntries.PresenterAttached, $"{presenter.GetType().Name}({Name})"));
        }

        /// <summary>
        /// Detaches the <see cref="IPresenter"/> that controls the view.
        /// </summary>
        public void Detach()
        {
            childView.Detach();

            if (presenter != null)
            {
                var entry = $"{presenter.GetType().Name}({Name})";

                presenter = null;

                log.Debug(string.Format(LogEntries.PresenterDetached, entry));
            }
        }

        /// <summary>
        /// Shows the tool window in the specified <see cref="DockPanel"/>.
        /// </summary>
        /// <param name="dockPanel">The <see cref="DockPanel"/> that will contain the tool window.</param>
        public new void Show(DockPanel dockPanel)
        {
            Debug.Assert(presenter != null);

            if (DockState == DockState.Hidden || DockState == DockState.Unknown)
            {
                // TODO - Finish this or remove it
                //Height = presenter.Height;
                //Width = presenter.Width;
            }

            Show(dockPanel, (DockState)Enum.Parse(DockState.GetType(), presenter.Location));
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
        /// Displays a message box with the specified caption, message, message type and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
            return DialogController.ShowMessage(this, caption, message, type, responses);
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
        /// Gets the persistence data that will be saved with the layout.
        /// </summary>
        /// <returns>The view ID.</returns>
        protected override string GetPersistString()
        {
            return Name;
        }

        /// <summary>
        /// Event handler for the <see cref="Form.Activated"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void Form_Activated(object sender, EventArgs e)
        {
            Debug.Assert(presenter != null);

            presenter.ViewActivated();
        }
    }
}
