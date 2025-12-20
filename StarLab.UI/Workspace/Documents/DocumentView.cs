using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared.Properties;
using Stratosoft.Commands;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.UI.Workspace.Documents
{
    /// <summary>
    /// A <see cref="DockContent"/> that implements the behaviour that is common to all document windows.
    /// </summary>
    public sealed partial class DocumentView : DockContent, IDocumentView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DocumentView)); // The logger that will be used for writing log messages.

        private readonly string id; // The view ID.

        private IDockableViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="DocumentView"/> class.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that this view represents.</param>
        public DocumentView(IDocument document)
        {
            ArgumentNullException.ThrowIfNull(document, nameof(document));

            InitializeComponent();

            Name = document.Name;
            Text = document.Name;
            id = document.ID;

            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.InstanceCreated, $"{nameof(DocumentView)}({ID})"));
        }

        /// <summary>
        /// Gets the <see cref="IViewController"/> that controls this view.
        /// </summary>
        public IViewController? Controller => (IViewController?)presenter;

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
        /// Attaches the <see cref="IPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IPresenter"/> that controls the view.</param>
        public void Attach(IPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(); // TODO

            this.presenter = (IDockableViewPresenter)presenter;
        }

        /// <summary>
        /// Attaches the <see cref="IChildView"/> to the view.
        /// </summary>
        /// <param name="view">The <see cref="IChildView"/> to attach.</param>
        public void Attach(IChildView view)
        {
            splitContainer.AddControl((Control)view, view.Panel);

            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.ViewAttached, view.Name, $"{nameof(DocumentView)}({ID})"));
        }

        /// <summary>
        /// Detaches the <see cref="IPresenter"/> that controls the view.
        /// </summary>
        public void Detach()
        {
            presenter = null;
        }

        /// <summary>
        /// Gets the specified <see cref="IChildViewController"/>.
        /// </summary>
        /// <param name="name">The name of the required controller.</param>
        /// <returns>The required <see cref="IChildViewController"/>.</returns>
        public IChildViewController GetController(string name)
        {
            foreach (var control in splitContainer.Panel1.Controls)
            {
                if (control is IChildView content && content.Controller != null && content.Controller.Name == name)
                {
                    return content.Controller;
                }
            }

            foreach (var control in splitContainer.Panel2.Controls)
            {
                if (control is IChildView content && content.Controller != null && content.Controller.Name == name)
                {
                    return content.Controller;
                }
            }

            throw new IndexOutOfRangeException(); // TODO
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
                        Debug.Assert(content.Controller != null);
                        content.Controller.RegisterController(parentController);
                        content.Controller.Initialise(controller);
                    }
                }

                foreach (var control in splitContainer.Panel2.Controls)
                {
                    if (control is IChildView content)
                    {
                        Debug.Assert(content.Controller != null);
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