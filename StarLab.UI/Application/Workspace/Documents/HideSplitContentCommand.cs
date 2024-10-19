using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents
{
    internal class HideSplitContentCommand : ComponentCommand<IDocumentController>
    {
        private readonly string target;

        public HideSplitContentCommand(ICommandManager commands, IDocumentController controller, string target)
            : base(commands, controller)
        {
            this.target = target;
        }

        public override void Execute()
        {
            receiver.HideSplitContent(target);
        }
    }
}
