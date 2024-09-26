using StarLab.Commands;

namespace StarLab.Application.Workspace
{
    internal class WorkspaceCommand : ComponentCommand<IWorkspaceController>
    {
        private readonly string target;

        private readonly string verb;

        public WorkspaceCommand(ICommandManager commands, IWorkspaceController controller, string verb, string target)
            : base(commands, controller)
        {
            this.target = target;
            this.verb = verb;
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
