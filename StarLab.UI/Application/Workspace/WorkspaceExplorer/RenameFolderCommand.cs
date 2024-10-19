using StarLab.Commands;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class RenameFolderCommand : ComponentCommand<IWorkspaceExplorerController>
    {
        private readonly string target;

        public RenameFolderCommand(ICommandManager commands, IWorkspaceExplorerController controller, string target)
            : base(commands, controller)
        {
            this.target = target;
        }

        public override void Execute()
        {
            receiver.Rename(target);
        }
    }
}
