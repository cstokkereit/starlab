using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    internal class ChartSettingsCommand : ComponentCommand<IChartSettingsController>
    {
        private readonly string target;

        private readonly string verb;

        public ChartSettingsCommand(ICommandManager commands, IChartSettingsController controller, string verb, string target)
            : base(commands, controller)
        {
            this.target = target;
            this.verb = verb;
        }

        public override void Execute()
        {
            switch (verb)
            {
                case Verbs.APPLY:
                    //receiver.
                    break;
            }
        }
    }
}
