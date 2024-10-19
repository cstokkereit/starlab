using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    internal class ApplySettingsCommand : ComponentCommand<IChartSettingsController>
    {
        //private readonly string target;

        public ApplySettingsCommand(ICommandManager commands, IChartSettingsController controller)
            : base(commands, controller)
        {
            //this.target = target;
        }

        public override void Execute()
        {
            //receiver.(target);
        }
    }
}
