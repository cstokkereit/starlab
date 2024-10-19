using StarLab.Commands;

namespace StarLab.Application.Workspace
{
    internal class CloseWorkspaceCommand : ComponentCommand<IWorkspaceController>
    {
        public CloseWorkspaceCommand(ICommandManager commands, IWorkspaceController controller)
            : base(commands, controller) { }

        public override void Execute()
        {
            receiver.CloseWorkspace();
        }
    }
}
