using StarLab.Presentation;
using StarLab.Presentation.Model;

namespace StarLab.UI
{
    public class ToolViewContext : IViewContext
    {
        private readonly IContent content;

        private readonly string defaultLocation;

        private readonly string name;

        private readonly string text;

        public ToolViewContext(string name, string text, string defaultLocation, IContent content)
        {
            this.defaultLocation = defaultLocation;
            this.content = content;
            this.name = name;
            this.text = text;
        }

        public IContent Content => content;

        public string DefaultLocation => defaultLocation;

        public string FullName => Name;

        public string Name => name;

        public string Text => text;

        public string View => Views.TOOL;
    }
}
