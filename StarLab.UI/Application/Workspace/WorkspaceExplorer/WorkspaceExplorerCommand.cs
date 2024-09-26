using StarLab.Commands;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class WorkspaceExplorerCommand : ComponentCommand<IWorkspaceExplorerController>
    {
        private readonly string target;

        private readonly string verb;

        public WorkspaceExplorerCommand(ICommandManager commands, IWorkspaceExplorerController controller, string verb, string target)
            : base(commands, controller)
        {
            this.target = target;
            this.verb = verb;
        }

        public override void Execute()
        {
            switch (verb)
            {
                case Verbs.COLLAPSE_ALL:
                    receiver.CollapseAll();
                    break;

                case Verbs.OPEN:
                    receiver.OpenDocument(target);
                    break;

                case Verbs.RENAME:
                    receiver.Rename(target);
                    break;

                case Verbs.SYNCHRONISE:
                    receiver.Synchronise();
                    break;
            }
        }
    }
}
