using StarLab.Commands;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class RenameDocumentCommand : ComponentCommand<IWorkspaceExplorerController>
    {
        private readonly string target;

        public RenameDocumentCommand(ICommandManager commands, IWorkspaceExplorerController controller, string target)
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
