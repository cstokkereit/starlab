namespace StarLab.Application.Configuration
{
    internal readonly struct ContentConfiguration : IContentConfiguration
    {
        private readonly int panel;

        private readonly string name;

        private readonly string presenter;

        private readonly string view;

        public ContentConfiguration(Content content)
        {
            panel = string.IsNullOrEmpty(content.Panel) ? 0 : int.Parse(content.Panel);
            presenter = string.Empty + content.Presenter;
            name = string.Empty + content.Name;
            view = string.Empty + content.View;
        }

        public string Name => name;

        public int Panel => panel;

        public string Presenter => presenter;

        public string View => view;
    }
}
