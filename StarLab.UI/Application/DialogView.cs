using log4net;
using StarLab.Application;

namespace StarLab
{
    /// <summary>
    /// The base class for all <see cref="Form"/> views.
    /// </summary>
    public partial class DialogView : Form, IDialogView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DialogView));

        private readonly IDialogViewPresenter presenter;

        private readonly string id;

        /// <summary>
        /// Initialises a new instance of the <see cref="DialogView"> class.
        /// </summary>
        public DialogView(string id, string text, IControlView content, IPresenterFactory factory)
        {
            ArgumentNullException.ThrowIfNull(content, nameof(content));
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));

            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;

            this.id = id;

            Text = text;
            Name = id;

            try
            {
                presenter = factory.CreatePresenter(this);
            }
            catch (Exception e)
            {
                log.Fatal(e.Message, e);
                throw;
            }

            SuspendLayout();

            if (content is Control control)
            {
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }

            ResumeLayout();
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="name">The view name.</param>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public virtual void Initialise(IApplicationController controller)
        {
            presenter.Initialise(controller);

            if (presenter is IDialogController parentController)
            {
                foreach (var control in Controls)
                {
                    if (control is IFormContent<IDialogController> view) view.Initialise(controller, parentController);
                }
            }
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
