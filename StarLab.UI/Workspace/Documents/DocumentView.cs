using log4net;
using StarLab.Application;
using StarLab.Presentation;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using Stratosoft.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.UI.Workspace.Documents
{
    /// <summary>
    /// A <see cref="DockContent"/> that implements the behaviour that is common to all document windows.
    /// </summary>
    public sealed partial class DocumentView : DockContent, IDocumentView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DocumentView)); // The logger that will be used for writing log messages.

        private readonly IDockableViewPresenter presenter; // The presenter that controls the view.

        private readonly string id; // The view ID.

        /// <summary>
        /// Initialises a new instance of the <see cref="DocumentView"/> class.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that this view represents.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the presenter and child view.</param>
        /// <param name="configuration">An <see cref="IViewConfiguration"/> that holds the configuration information required to construct this view.</param>
        public DocumentView(IDocument document, IViewFactory factory, IViewConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(document, nameof(document));
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

            InitializeComponent();

            Name = document.Name;
            Text = document.Name;
            id = document.ID;

            presenter = factory.CreatePresenter(document, this);

            foreach (var content in configuration.ChildViews)
            {
                var view = factory.CreateView(content, configuration);
                splitContainer.AddControl((Control)view, view.Panel);
            }
        }

        /// <summary>
        /// Gets the <see cref="IViewController"/> that controls this view.
        /// </summary>
        public IViewController Controller => (IViewController)presenter;

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public string ID => id;

        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The <see cref="Image"/> to use for the button.</param>
        /// <param name="command">The <see cref="ICommand"/> to invoke when the button is clicked.</param>
        public void AddToolbarButton(string name, string tooltip, Image image, ICommand command)
        {
            splitContainer.AddToolbarButton(name, tooltip, image, command);
        }

        /// <summary>
        /// Hides the specified split content.
        /// </summary>
        /// <param name="name">The name of the content to be hidden.</param>
        public void HideSplitContent(string name)
        {
            splitContainer.HideSplitContent(name);
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            if (presenter is IViewController parentController)
            {
                foreach (var control in splitContainer.Panel1.Controls)
                {
                    if (control is IChildView content)
                    {
                        content.Controller.RegisterController(parentController);
                        content.Controller.Initialise(controller);
                    }
                }

                foreach (var control in splitContainer.Panel2.Controls)
                {
                    if (control is IChildView content)
                    {
                        content.Controller.RegisterController(parentController);
                        content.Controller.Initialise(controller);
                    }
                }
            }
        }

        /// <summary>
        /// Shows the tool window in the specified <see cref="DockPanel"/>.
        /// </summary>
        /// <param name="dockPanel">The <see cref="DockPanel"/> that will contain the tool window.</param>
        public new void Show(DockPanel dockPanel)
        {
            if (DockState == DockState.Hidden || DockState == DockState.Unknown)
            {
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
        /// Displays a <see cref="MessageBox"/> with the specified options.
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
        /// <param name="owner">The <see cref="IView"/> that will own the message box.</param>
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
        /// Shows the specified split content.
        /// </summary>
        /// <param name="name">The name of the content to be shown.</param>
        public void ShowSplitContent(string name)
        {
            splitContainer.ShowSplitContent(name);
        }

        /// <summary>
        /// Gets the persistence data that will be saved with the layout.
        /// </summary>
        /// <returns>The view ID.</returns>
        protected override string GetPersistString()
        {
            return ID;
        }
    }
}