using StarLab.Commands;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class CollapseAllCommand : ComponentCommand<IWorkspaceExplorerController>
    {
        public CollapseAllCommand(ICommandManager commands, IWorkspaceExplorerController controller)
            : base(commands, controller) { }

        public override void Execute()
        {
            receiver.CollapseAll();
        }
    }
}
