using StarLab.Commands;

namespace StarLab.Application
{
    internal class SplitViewCommand : ComponentCommand<ISplitViewController>
    {
        private readonly string verb;
        private readonly string view;

        public SplitViewCommand(ICommandManager commands, ISplitViewController controller, string verb, string view)
            : base (commands, controller)
        {
            this.view = view;
            this.verb = verb;
        }

        public override void Execute()
        {
            switch (verb)
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
