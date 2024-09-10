using StarLab.Commands;
using StarLab.Presentation.Charts;
using StarLab.Presentation.Workspaces;
using StarLab.Presentation.Workspaces.WorkspaceExplorer;

namespace StarLab.Presentation
{
    public interface IApplicationController
    {
        ICommand CreateAggregateCommand(IEnumerable<ICommand> commands, string view);

        ICommand GetCommand(ControllerAction<IChartSettingsController> action);

        ICommand GetCommand(ControllerAction<ISplitViewController> action);

        ICommand GetCommand(ControllerAction<IViewController> action);

        ICommand GetCommand(ControllerAction<IWorkspaceController> action);

        ICommand GetCommand(ControllerAction<IWorkspaceExplorerController> action);

        /// <summary>
        /// Starts the application.
        /// </summary>
        void Run();
    }
}
