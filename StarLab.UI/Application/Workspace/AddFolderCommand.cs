using StarLab.Commands;

namespace StarLab.Application.Workspace
{
    internal class AddFolderCommand : ComponentCommand<IWorkspaceController>
    {
        private readonly string target;

        public AddFolderCommand(ICommandManager commands, IWorkspaceController controller, string target)
            : base(commands, controller)
        {
            this.target = target;
        }

        public override void Execute()
        {
            receiver.AddFolder(target);
        }
    }
}
