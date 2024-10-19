using StarLab.Commands;

namespace StarLab.Application.Workspace
{
    internal class DeleteDocumentCommand : ComponentCommand<IWorkspaceController>
    {
        private readonly string target;

        public DeleteDocumentCommand(ICommandManager commands, IWorkspaceController controller, string target)
            : base(commands, controller)
        {
            this.target = target;
        }

        public override void Execute()
        {
            receiver.DeleteDocument(target);
        }
    }
}
