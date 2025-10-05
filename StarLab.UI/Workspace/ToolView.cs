using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace;
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

        private readonly string id; // The view ID.

        private IChildView? childView; // A view that implements the tool specific behaviour.

        private IDockableViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="ToolView"> class.
        /// </summary>
        /// <param name="name">The name of the tool window.</param>
        /// <param name="text">The tool window text.</param>
        /// <param name="definition">The <see cref="ViewDefinition"/> used to construct this view.</param>
        /// <exception cref="ArgumentException"></exception>
        public ToolView(string name, string text, ViewDefinition definition)
        {
            ArgumentNullException.ThrowIfNull(definition, nameof(definition));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(text, nameof(text));

            Debug.Assert(definition.ChildViewDefinitions.Count == 1);

            InitializeComponent();

            Name = name;
            Text = text;
            id = name;

            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.InstanceCreated, nameof(ToolView)));
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
        /// <param name="childView">The <see cref="IChildView"/> to attach.</param>
        public void Attach(IChildView childView)
        {
            ArgumentNullException.ThrowIfNull(nameof(childView));

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
            else
            {
                throw new ArgumentException(string.Format(Resources.ChildViewNotAControl, childView.GetType()));
            }

            ResumeLayout();

            this.childView = childView;

            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.ViewAttached, childView.Name, nameof(ToolView)));
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
        /// Shows the tool window in the specified <see cref="DockPanel"/>.
        /// </summary>
        /// <param name="dockPanel">The <see cref="DockPanel"/> that will contain the tool window.</param>
        public new void Show(DockPanel dockPanel)
        {
            if (DockState == DockState.Hidden || DockState == DockState.Unknown)
            {
                // TODO - Finish this or remove it
                //Height = presenter.Height;
                //Width = presenter.Width;
            }

            Debug.Assert(presenter != null);

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
        /// Gets the persistence data that will be saved with the layout.
        /// </summary>
        /// <returns>The view ID.</returns>
        protected override string GetPersistString()
        {
            return ID;
        }
    }
}
