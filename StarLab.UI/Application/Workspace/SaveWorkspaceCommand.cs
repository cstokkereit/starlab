using StarLab.Commands;

namespace StarLab.Application.Workspace
{
    internal class SaveWorkspaceCommand : ComponentCommand<IWorkspaceController>
    {
        public SaveWorkspaceCommand(ICommandManager commands, IWorkspaceController controller)
            : base(commands, controller) { }

        public override void Execute()
        {
            receiver.SaveWorkspace();
        }
    }
}
