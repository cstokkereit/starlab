using StarLab.Commands;

namespace StarLab.Application.Workspace
{
    internal class CloseDocumentCommand : ComponentCommand<IWorkspaceController>
    {
        public CloseDocumentCommand(ICommandManager commands, IWorkspaceController controller)
            : base(commands, controller) { }

        public override void Execute()
        {
            receiver.CloseActiveDocument();
        }
    }
}
