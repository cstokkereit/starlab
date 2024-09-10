namespace StarLab.Presentation
{
    /// <summary>
    /// This is the base interface for all views.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// 
        /// </summary>
        Size MinimumSize { get; set; }

        /// <summary>
        /// Gets or sets the view text.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the view text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="name">The view name.</param>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        void Initialise(IApplicationController controller);

        /// <summary>
        /// Shows the specified view.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        void Show(IView view);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons and icon.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="buttons">A <see cref="MessageBoxButtons"/> that specifies which buttons to include on the meeage box.</param>
        /// <param name="icon">A <see cref="MessageBoxIcon"/> that specifies the icon to include on the meeage box.</param>
        /// <returns>A <see cref="DialogResult"/> that identifies the button that was clicked.</returns>
        DialogResult ShowMessage(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon);

        /// <summary>
        /// Displays a message box with the specified text, caption and icon.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="icon">A <see cref="MessageBoxIcon"/> that specifies the icon to include on the meeage box.</param>
        void ShowMessage(string caption, string message, MessageBoxIcon icon);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        string ShowOpenFileDialog(string title, string filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        string ShowSaveFileDialog(string title, string filter, string extension);
    }
}
