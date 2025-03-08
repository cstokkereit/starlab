using log4net;
using StarLab.Application.Configuration;
using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A <see cref="DockContent"/> that implements the behaviour that is common to all dockable tool windows.
    /// </summary>
    public sealed partial class ToolView : DockContent, IDockableView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ToolView)); // The logger that will be used for writing log messages.

        private readonly IDockableViewPresenter presenter; // The presenter that controls the view.

        private readonly IChildView content; // TODO

        private readonly string id; // The view ID.

        /// <summary>
        /// Initialises a new instance of the <see cref="ToolView"> class.
        /// </summary>
        /// <param name="name">The name of the tool window.</param>
        /// <param name="text">The tool window text.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the presenter and child view.</param>
        /// <param name="configuration">An <see cref="IViewConfiguration"/> that holds the configuration information required to construct this view.</param>
        /// <exception cref="ArgumentException"></exception>
        public ToolView(string name, string text, IViewFactory factory, IViewConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(text, nameof(text));

            if (configuration.ChildViews.Count > 1) throw new ArgumentException(); // TODO

            InitializeComponent();

            Name = name;
            Text = text;
            id = name;
   
            SuspendLayout();

            presenter = (IDockableViewPresenter)factory.CreatePresenter(configuration.Name, this);

            content = factory.CreateView(configuration.ChildViews[0], configuration);

            content.Controller.RegisterController((IViewController)presenter);

            if (content is Control control)
            {
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }
            else
            {
                throw new Exception(); // TODO - This should never happen
            }

            ResumeLayout();
        }

        /// <summary>
        /// Gets the <see cref="IChildViewController"/> that controls the view content.
        /// </summary>
        //public IChildViewController ContentController => content.Controller; TODO - Remove if never needed

        /// <summary>
        /// Gets the <see cref="IViewController"/> that controls this view.
        /// </summary>
        public IViewController Controller => (IViewController)presenter;

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public string ID => id;

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            content.Controller.Initialise(controller);
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
        /// Gets the persistence data that will be saved with the layout.
        /// </summary>
        /// <returns>The view ID.</returns>
        protected override string GetPersistString()
        {
            return ID;
        }
    }
}
