using StarLab.Application;
using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a controller that can be used to create, initialise and manage the views that comprise the user interface of the application.
    /// </summary>
    public interface IApplicationController : IController
    {
        /// <summary>
        /// Deletes the <see cref="IView"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IView"/> to delete.</param>
        void DeleteView(string id);

        /// <summary>
        /// Exits the application.
        /// </summary>
        void Exit();

        /// <summary>
        /// Gets the specified <see cref="IDocumentController"/>.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that identifies the required controller.</param>
        /// <returns>The specified <see cref="IDocumentController"/>.</returns>
        IDocumentController GetController(IDocument document);

        /// <summary>
        /// Gets the specified output port.
        /// </summary>
        /// <typeparam name="TOutputPort">The type of output port required.</typeparam>
        /// <param name="id">The ID of the parent controller.</param>
        /// <returns>The specified output port.</returns>
        TOutputPort GetOutputPort<TOutputPort>(string id);

        /// <summary>
        /// Gets the specified output port.
        /// </summary>
        /// <typeparam name="TOutputPort">The type of output port required.</typeparam>
        /// <returns>The specified output port.</returns>
        TOutputPort GetOutputPort<TOutputPort>();

        /// <summary>
        /// Gets the <see cref="IView"/> specified by the <see cref="IDocument"/> provided. If the view does not already exist it will be created.
        /// </summary>
        /// <param name="document">An instance of <see cref="IDocument"/> that specifies which instance of <see cref="IView"/> is required.</param>
        /// <returns>The required <see cref="IView">.</returns>
        IView GetView(IDocument document);

        /// <summary>
        /// Gets the <see cref="IView"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the required <see cref="IView"/>.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        IView GetView(string id);

        /// <summary>
        /// Starts the application.
        /// </summary>
        void Run();

        /// <summary>
        /// Shows the About dialog.
        /// </summary>
        void ShowAboutDialog();

        /// <summary>
        /// Shows a Document window that contains the <see cref="IDocument"/> provided.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> to show.</param>
        void ShowDocument(IDocument document);

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
        /// Displays a <see cref="MessageBox"/> with the specified caption, message and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        InteractionResult ShowMessage(string caption, string message, InteractionResponses responses);

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified caption and message.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        void ShowMessage(string caption, string message);

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified owner and options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        string ShowOpenFileDialog(string title, string filter);

        /// <summary>
        /// Shows the Options dialog.
        /// </summary>
        void ShowOptionsDialog();

        /// <summary>
        /// Displays a save file dialog with the specified owner and options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        string ShowSaveFileDialog(string title, string filter, string extension);

        /// <summary>
        /// Shows the Workspace Explorer.
        /// </summary>
        void ShowWorkspaceExplorer();
    }
}
