using StarLab.Commands;

namespace StarLab.Application
{
    internal class ExitApplicationCommand : ComponentCommand<IApplicationController>
    {
        public ExitApplicationCommand(ICommandManager commands, IApplicationController controller)
            : base(commands, controller) { }

        public override void Execute()
        {
            receiver.Exit();
        }
    }
}
