using StarLab.Commands;

namespace StarLab.Application
{
    /// <summary>
    /// A command that shows the specified view.
    /// </summary>
    internal class ShowCommand : ComponentCommand<IViewController>
    {
        private readonly IView view;

        public ShowCommand(ICommandManager commands, IViewController controller, IView view)
            : base(commands, controller)
        {
            this.view = view;
        }

        public override void Execute()
        {
            receiver.Show(view);
        }
    }
}
