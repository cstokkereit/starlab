namespace StarLab.Application.Workspace.Documents
{
    public class Content : IContent
    {
        private readonly string name;

        private readonly SplitViewPanels panel;

        private readonly string view;

        public Content(string view, string name, SplitViewPanels panel)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.view = view ?? throw new ArgumentNullException(nameof(view));

            this.panel = panel;
        }

        public Content(ContentDTO dto)
        {
            name = dto.Name ?? throw new ArgumentException();
            view = dto.View ?? throw new ArgumentException();

            panel = (SplitViewPanels)dto.Panel;
        }

        public string Name => name;

        public SplitViewPanels Panel => panel;

        public string View => view;
    }
}
