using StarLab.Application;

namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a parent view.
    /// </summary>
    public interface IParentView : IView
    {
        /// <summary>
        /// Attaches the <see cref="IPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IPresenter"/> that controls the view.</param>
        void Attach(IPresenter presenter);

        /// <summary>
        /// Detaches the presenter that controls the view.
        /// </summary>
        void Detach();

        /// <summary>
        /// Shows the specified view.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        void Show(IView view);

        /// <summary>
        /// Displays a message box with the specified caption, message, message type and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses);

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        string ShowOpenFileDialog(string title, string filter);

        /// <summary>
        /// Displays a <see cref="SaveFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        string ShowSaveFileDialog(string title, string filter, string extension);
    }
}
