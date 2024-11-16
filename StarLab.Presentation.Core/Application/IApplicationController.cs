using StarLab.Application.Workspace;
using StarLab.Commands;

namespace StarLab.Application
{
    public interface IApplicationController : IController
    {
        void Exit();

        ICommand CreateCommand(ICommandManager commands, IController controller, string action, string target);

        ICommand CreateCommand(ICommandManager commands, IController controller, string action);

        ICommand CreateCommand(ICommandManager commands, string view);

        IDialogView GetDialog(string name);

        IWorkspaceController GetWorkspaceController();

        void RegisterCommandInvokers(ICommandManager commands);

        /// <summary>
        /// Starts the application.
        /// </summary>
        void Run();

        void Show(IDialogView view);

        void Show(string id);
    }
}
