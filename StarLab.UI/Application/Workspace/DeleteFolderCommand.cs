using StarLab.Commands;

namespace StarLab.Application.Workspace
{
    internal class DeleteFolderCommand : ComponentCommand<IWorkspaceController>
    {
        private readonly string target;

        public DeleteFolderCommand(ICommandManager commands, IWorkspaceController controller, string target)
            : base(commands, controller)
        {
            this.target = target;
        }

        public override void Execute()
        {
            receiver.DeleteFolder(target);
        }
    }
}
