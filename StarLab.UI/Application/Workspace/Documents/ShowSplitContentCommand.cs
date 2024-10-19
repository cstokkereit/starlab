using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents
{
    internal class ShowSplitContentCommand : ComponentCommand<IDocumentController>
    {
        private readonly string target;

        public ShowSplitContentCommand(ICommandManager commands, IDocumentController controller, string target)
            : base(commands, controller)
        {
            this.target = target;
        }

        public override void Execute()
        {
            receiver.ShowSplitContent(target);
        }
    }
}
