using StarLab.Commands;

namespace StarLab.Application.Workspace
{
    internal class OpenWorkspaceCommand : ComponentCommand<IWorkspaceController>
    {
        public OpenWorkspaceCommand(ICommandManager commands, IWorkspaceController controller)
            : base(commands, controller) { }

        public override void Execute()
        {
            receiver.OpenWorkspace();
        }
    }
}
