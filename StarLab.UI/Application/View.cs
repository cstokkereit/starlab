using log4net;
using StarLab.Application;

namespace StarLab
{
    /// <summary>
    /// The base class for all <see cref="Form"/> views.
    /// </summary>
    public partial class View : Form, IFormView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(View));

        private readonly IFormViewPresenter presenter;

        private readonly string id;

        /// <summary>
        /// Initialises a new instance of the <see cref="View"> class.
        /// </summary>
        public View(string id, string name, IControlView content, IPresenterFactory factory)
        {
            ArgumentNullException.ThrowIfNull(nameof(content));
            ArgumentNullException.ThrowIfNull(nameof(factory));

            InitializeComponent();

            if (content is UserControl control) Controls.Add(control);

            this.id = id;

            Name = name;

            try
            {
                presenter = factory.CreatePresenter(this);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message, ex);
                throw;
            }

            StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="name">The view name.</param>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public virtual void Initialise(IApplicationController controller)
        {
            presenter.Initialise(controller);
        }

        public IViewController Controller => (IViewController)presenter;

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
    }
}
