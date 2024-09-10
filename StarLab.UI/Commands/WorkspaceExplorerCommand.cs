using StarLab.Commands;
using StarLab.Presentation;
using StarLab.Presentation.Workspaces.WorkspaceExplorer;

namespace StarLab.UI.Commands
{
    internal class WorkspaceExplorerCommand : ComponentCommand<IWorkspaceExplorerController>
    {
        private readonly string verb;

        public WorkspaceExplorerCommand(ICommandManager commands, ControllerAction<IWorkspaceExplorerController> action)
            : base(commands, action.Controller)
        {
            verb = action.Verb;
        }

        public override void Execute()
        {
            switch (verb)
            {
                case Verbs.COLLAPSE_ALL:
                    receiver.CollapseAll();
                    break;

                case Verbs.SYNCHRONISE:
                    receiver.Synchronise();
                    break;
            }
        }
    }
}
