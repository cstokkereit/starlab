using StarLab.Commands;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class SynchroniseCommand : ComponentCommand<IWorkspaceExplorerController>
    {
        public SynchroniseCommand(ICommandManager commands, IWorkspaceExplorerController controller)
            : base(commands, controller) { }

        public override void Execute()
        {
            receiver.Synchronise();
        }
    }
}
