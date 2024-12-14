using log4net;
using StarLab.Application.Configuration;
using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.Application.Workspace
{
    public sealed partial class ToolView : DockContent, IDockableView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ToolView));

        private readonly IDockableViewPresenter presenter;

        private readonly string id;

        public ToolView(string name, string text, IViewFactory factory, IViewConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            ArgumentNullException.ThrowIfNull(config, nameof(config));

            if (config.Contents.Count > 1) throw new ArgumentException(); // TODO

            InitializeComponent();

            Name = name;
            Text = text;
            id = name;
   
            SuspendLayout();

            presenter = (IDockableViewPresenter)factory.CreatePresenter(config.Name, this);

            var view = factory.CreateView(config.Contents[0], config);
            view.Controller.Attach((IViewController)presenter);

            view.Controller.Attach((IViewController)presenter);

            if (view is Control control)
            {
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }

            ResumeLayout();
        }

        public IViewController Controller => (IViewController)presenter;

        public string ID => id;

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            foreach (var control in Controls)
            {
                if (control is IChildView view) view.Controller.Initialise(controller);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dockPanel"></param>
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
        /// Displays a message box with the specified text, caption, buttons and icon.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="buttons">A <see cref="MessageBoxButtons"/> that specifies which buttons to include on the meeage box.</param>
        /// <param name="icon">A <see cref="MessageBoxIcon"/> that specifies the icon to include on the meeage box.</param>
        /// <returns>A <see cref="DialogResult"/> that identifies the button that was clicked.</returns>
        public DialogResult ShowMessage(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return DialogController.ShowMessage(this, caption, message, buttons, icon);
        }

        /// <summary>
        /// Displays a message box with the specified text, caption and icon.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="icon">A <see cref="MessageBoxIcon"/> that specifies the icon to include on the meeage box.</param>
        public void ShowMessage(string caption, string message, MessageBoxIcon icon)
        {
            DialogController.ShowMessage(this, caption, message, icon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            return DialogController.ShowOpenFileDialog(this, title, filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return DialogController.ShowSaveFileDialog(this, title, filter, extension);
        }

        protected override string GetPersistString()
        {
            return ID;
        }
    }
}
