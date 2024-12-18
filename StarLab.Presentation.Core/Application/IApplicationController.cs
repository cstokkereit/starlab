using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Commands;

namespace StarLab.Application
{
    /// <summary>
    /// Represents a controller that creates, initialises and manages the views that comprise the user interface of the application.
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
        /// <param name="controller">The <see cref="IController"/> that contains the method that will be invoked by the <see cref="ICommand"/> when it's <see cref="Execute"/> method is called.</param>
        /// <param name="action">The action to be performed by the <see cref="ICommand"/> when it's <see cref="Execute"/> method is called.</param>
        /// <param name="target">The target for the action.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to invoke the specified action.</returns>
        ICommand CreateCommand(ICommandManager commands, IController controller, string action, string target);

        /// <summary>
        /// Creates the <see cref="ICommand"> specified by the controller and action provided.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of the command.</param>
        /// <param name="controller">The <see cref="IController"/> that contains the method that will be invoked by the <see cref="ICommand"/> when it's <see cref="Execute"/> method is called.</param>
        /// <param name="action">The action to be performed by the <see cref="ICommand"/> when it's <see cref="Execute"/> method is called.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to invoke the specified action.</returns>
        ICommand CreateCommand(ICommandManager commands, IController controller, string action);

        /// <summary>
        /// Creates an <see cref="ICommand"> that will show the specified view.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of the command.</param>
        /// <param name="view">The name of the <see cref="IView"/> to be shown.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to show the specified view.</returns>
        ICommand CreateCommand(ICommandManager commands, string view);

        /// <summary>
        /// Gets the <see cref="IView"/> specified by the <see cref="IDocument"/> provided. If the view does not already exist it will be created.
        /// </summary>
        /// <param name="document">An instance of <see cref="IDocument"/> that specifies which instance of <see cref="IView"/> is required.</param>
        /// <returns>The required instance of <see cref="IView">.</returns>
        IView GetView(IDocument document);

        /// <summary>
        /// Gets the <see cref="IView"/> with the specified ID. A view with the specified ID must already exist or an exception will be thrown.
        /// </summary>
        /// <param name="id">The ID of the required instance of <see cref="IView"/>.</param>
        /// <returns>The required instance of <see cref="IView">.</returns>
        IView GetView(string id);

        /// <summary>
        /// TODO - This may be replaced by an indexed element e.g. Controllers[id]
        /// </summary>
        /// <returns></returns>
        IWorkspaceController GetWorkspaceController();

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
        /// Shows the <see cref="IView"> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"> to be shown.</param>
        void Show(IView view);

        /// <summary>
        /// Shows the <see cref="IView"> with the specified ID. A view with the specified ID must already exist or an exception will be thrown.
        /// </summary>
        /// <param name="id">The ID of the view to be shown.</param>
        void Show(string id);
    }
}
