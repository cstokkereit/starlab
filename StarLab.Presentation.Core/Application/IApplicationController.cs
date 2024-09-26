using StarLab.Application.Workspace;
using StarLab.Commands;

namespace StarLab.Application
{
    public interface IApplicationController : IController
    {
        ICommand CreateAggregateCommand(IEnumerable<ICommand> commands, string view);

        ICommand GetCommand(IController controller, string verb, string target);

        ICommand GetCommand(IController controller, string verb);

        ICommand GetShowCommand(IViewController controller, string view);

        IWorkspaceController GetWorkspaceController();

        /// <summary>
        /// Starts the application.
        /// </summary>
        void Run();
    }
}
