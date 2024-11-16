using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents
{
    internal class ToolbarButton : IToolbarButton
    {
        private readonly ICommand command;

        private readonly string name;

        private readonly string tooltip;

        private readonly Image image;

        public ToolbarButton(string name, string tooltip, Image image, ICommand command)
        {
            this.command = command;
            this.tooltip = tooltip;
            this.image = image;
            this.name = name;
        }

        public ICommand Command => command;

        public string Name => name;

        public Image Image => image;

        public string Tooltip => tooltip;
    }
}
