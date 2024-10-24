using StarLab.Application.Workspace;
using StarLab.Commands;

namespace StarLab.Application
{
    public interface IApplicationController : IController
    {
        void Exit();

        ICommand GetCommand(ICommandManager commands, IController controller, string action, string target);

        ICommand GetCommand(ICommandManager commands, IController controller, string action);

        ICommand GetCommand(ICommandManager commands, IViewController controller, string view);

        IWorkspaceController GetWorkspaceController();

        void RegisterCommandInvokers(ICommandManager commands);

        /// <summary>
        /// Starts the application.
        /// </summary>
        void Run();

        void Show(string id);
    }
}
