using log4net;
using StarLab.Application;
using StarLab.Application.Configuration;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using System.Windows.Forms;

namespace StarLab
{
    /// <summary>
    /// The base class for all <see cref="Form"/> based views.
    /// </summary>
    public partial class DialogView : Form, IDialog, IDialogView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DialogView));

        private readonly IDialogViewPresenter presenter;

        private readonly IChildView childView;

        private readonly string id;

        /// <summary>
        /// Initialises a new instance of the <see cref="DialogView"> class.
        /// </summary>
        public DialogView(string name, string text, IViewFactory factory, IViewConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            ArgumentNullException.ThrowIfNull(config, nameof(config));

            if (config.Contents.Count > 1) throw new ArgumentException(); // TODO

            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;

            Name = name;
            Text = text;
            id = name;

            presenter = (IDialogViewPresenter)factory.CreatePresenter(config.Name, this);

            childView = factory.CreateView(config.Contents[0], config);
            childView.Controller.Attach((IViewController)presenter);

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

        public void Show(IInteractionContext context)
        {
            childView.Controller.Run(context);
        }

        public IViewController Controller => (IViewController)presenter;

        public bool HideOnClose { get; set; }

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

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (HideOnClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
