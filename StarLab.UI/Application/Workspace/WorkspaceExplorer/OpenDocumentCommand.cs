using StarLab.Commands;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class OpenDocumentCommand : ComponentCommand<IWorkspaceExplorerController>
    {
        private readonly string target;

        public OpenDocumentCommand(ICommandManager commands, IWorkspaceExplorerController controller, string target)
            : base(commands, controller)
        {
            this.target = target;
        }

        public override void Execute()
        {
            receiver.OpenDocument(target);
        }
    }
}