using StarLab.Commands;

namespace StarLab.Application.Workspace
{
    internal class NewWorkspaceCommand : ComponentCommand<IWorkspaceController>
    {
        public NewWorkspaceCommand(ICommandManager commands, IWorkspaceController controller)
            : base(commands, controller) { }

        public override void Execute()
        {
            receiver.NewWorkspace();
        }
    }
}
