using StarLab.Commands;
using StarLab.Presentation;

namespace StarLab.UI.Commands
{
    internal class SplitViewCommand : ComponentCommand<ISplitViewController>
    {
        private readonly string verb;
        private readonly string view;

        public SplitViewCommand(ICommandManager commands, ControllerAction<ISplitViewController> action)
            : base(commands, action.Controller)
        {
            view = action.Target;
            verb = action.Verb;
        }

        public override void Execute()
        {
            switch(verb)
            {
                case Verbs.COLLAPSE:
                    receiver.Collapse(view);
                    break;

                case Verbs.EXPAND:
                    receiver.Expand(view);
                    break;
            }
        }
    }
}
