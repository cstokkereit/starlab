using StarLab.Commands;
using StarLab.Presentation;
using StarLab.Presentation.Charts;

namespace StarLab.UI.Commands
{
    internal class ChartSettingsCommand : ComponentCommand<IChartSettingsController>
    {
        private readonly string verb;

        public ChartSettingsCommand(ICommandManager commands, ControllerAction<IChartSettingsController> action)
            : base(commands, action.Controller)
        {
            verb = action.Verb;
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
