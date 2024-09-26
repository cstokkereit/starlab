using StarLab.Application;

namespace StarLab
{
    /// <summary>
    /// The base class for all <see cref="Form"/> views.
    /// </summary>
    public abstract partial class View : Form
    { 
        /// <summary>
        /// Initialises a new instance of the <see cref="View"> class.
        /// </summary>
        public View()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="name">The view name.</param>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public virtual void Initialise(IApplicationController controller)
        {
            StartPosition = FormStartPosition.CenterParent;
        }

        #region IView Members

        public IViewController Controller => GetController();

        public string ID => Name;

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
            return MessageBox.Show(this, message, caption, buttons, icon);
        }

        /// <summary>
        /// Displays a message box with the specified text, caption and icon.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="icon">A <see cref="MessageBoxIcon"/> that specifies the icon to include on the meeage box.</param>
        public void ShowMessage(string caption, string message, MessageBoxIcon icon)
        {
            ShowMessage(message, caption, MessageBoxButtons.OK, icon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            var filename = string.Empty;

            var dialog = new OpenFileDialog
            {
                AddExtension = false,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = filter,
                Multiselect = false,
                Title = title,
                ValidateNames = true
            };

            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK) filename = dialog.FileName;

            return filename;
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
            var filename = string.Empty;

            var dialog = new SaveFileDialog
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = extension,
                Filter = filter,
                OverwritePrompt = true,
                Title = title,
                ValidateNames = true
            };

            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK) filename = dialog.FileName;

            return filename;
        }

        #endregion

        protected abstract IViewController GetController();
    }
}
