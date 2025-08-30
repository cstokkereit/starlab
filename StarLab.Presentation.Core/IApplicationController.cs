using StarLab.Application;
using StarLab.Presentation.Workspace.Documents;
using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a controller that can be used to create, initialise and manage the views that comprise the user interface of the application.
    /// </summary>
    public interface IApplicationController : IController
    {
        /// <summary>
        /// Exits the application.
        /// </summary>
        void Exit();

        /// <summary>
        /// Creates the <see cref="ICommand"> specified by the controller, action and target provided.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of the command.</param>
        /// <param name="controller">The <see cref="IController"/> that contains the method that will be invoked by the <see cref="ICommand"/> when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <param name="action">The action to be performed when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <param name="target">The target for the action.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to invoke the specified action.</returns>
        ICommand CreateCommand(ICommandManager commands, IController controller, string action, string target);

        /// <summary>
        /// Creates the <see cref="ICommand"/> specified by the controller and action provided.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of the command.</param>
        /// <param name="controller">The <see cref="IController"/> that contains the method that will be invoked by the <see cref="ICommand"/> when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <param name="action">The action to be performed when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <returns>An instance of <see cref="ICommand"/> that can be used to invoke the specified action.</returns>
        ICommand CreateCommand(ICommandManager commands, IController controller, string action);

        /// <summary>
        /// Creates an <see cref="ICommand"> that will show the specified view.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of the command.</param>
        /// <param name="view">The name of the <see cref="IView"/> to be shown.</param>
        /// <returns>An instance of <see cref="ICommand"/> that can be used to show the specified view.</returns>
        ICommand CreateCommand(ICommandManager commands, string view);

        /// <summary>
        /// Deletes the <see cref="IView"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IView"/> to delete.</param>
        void DeleteView(string id);

        /// <summary>
        /// Gets the <see cref="IChildViewController"/> that controls the content of the specified view.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <returns>The required <see cref="IChildViewController"/>.</returns>
        //IChildViewController GetContentController(string name);

        /// <summary>
        /// Gets the <see cref="IDocumentController"/> that controls the view representing the <see cref="IDocument"/> provided.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> represented by the view controlled by the <see cref="IDocumentController"/>.</param>
        /// <returns>The required <see cref="IDocumentController"/></returns>
        IDocumentController GetController(IDocument document);

        /// <summary>
        /// Gets the specified <see cref="IController"/>.
        /// </summary>
        /// <param name="name">The name of the controller.</param>
        /// <returns>The required <see cref="IController">.</returns>
        IController? GetController(string id);

        /// <summary>
        /// Gets the <see cref="IView"/> specified by the <see cref="IDocument"/> provided. If the view does not already exist it will be created.
        /// </summary>
        /// <param name="document">An instance of <see cref="IDocument"/> that specifies which instance of <see cref="IView"/> is required.</param>
        /// <returns>The required <see cref="IView"/>.</returns>
        IView GetView(IDocument document);

        /// <summary>
        /// Gets the <see cref="IView"/> with the specified ID. If the view does not exist <see cref="null"/> will be returned.
        /// </summary>
        /// <param name="id">The ID of the required <see cref="IView"/>.</param>
        /// <returns>The required <see cref="IView"/> or <see cref="null"/>.</returns>
        IView? GetView(string id);

        /// <summary> 
        /// Registers the available command invokers with the instance of <see cref="ICommandManager"/> provided.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that will be used to register the available command invokers.</param>
        void RegisterCommandInvokers(ICommandManager commands);

        /// <summary>
        /// Starts the application.
        /// </summary>
        void Run();

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"> to be shown.</param>
        void Show(IView view);

        /// <summary>
        /// Shows the <see cref="IView"/> with the specified ID. A view with the specified ID must already exist or an exception will be thrown.
        /// </summary>
        /// <param name="id">The ID of the view to be shown.</param>
        /// <exception cref="ViewNotFoundException"></exception>
        void Show(string id);

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified caption, message, message type and available responses.
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
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        InteractionResult ShowMessage(string caption, string message);
    }
}
