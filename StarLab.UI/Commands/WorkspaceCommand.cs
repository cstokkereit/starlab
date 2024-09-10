using StarLab.Commands;
using StarLab.Presentation;
using StarLab.Presentation.Workspaces;

namespace StarLab.UI.Commands
{
    internal class WorkspaceCommand : ComponentCommand<IWorkspaceController>
    {
        private readonly string verb;

        public WorkspaceCommand(ICommandManager commands, ControllerAction<IWorkspaceController> action)
            : base(commands, action.Controller)
        {
            verb = action.Verb;
        }

        public override void Execute()
        {
            switch (verb)
            {
                case Verbs.CLOSE:
                    receiver.CloseWorkspace();
                    break;

                case Verbs.NEW:
                    receiver.NewWorkspace();
                    break;

                case Verbs.OPEN:
                    receiver.OpenWorkspace();
                    break;

                case Verbs.SAVE:
                    receiver.SaveWorkspace();
                    break;
            }
        }
    }
}
