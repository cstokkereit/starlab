namespace StarLab.Application.Configuration
{
    internal struct ViewConfiguration : IViewConfiguration
    {
        private readonly Dictionary<string, IChildViewConfiguration> contentsByName = new Dictionary<string, IChildViewConfiguration>();

        private readonly List<IChildViewConfiguration> contents = new List<IChildViewConfiguration>();

        private readonly ViewTypes type;

        private readonly string name;

        public ViewConfiguration(View view)
        {
            name =  view.Name;

            switch (view.Type)
            {
                case Constants.APPLICATION:
                    type = ViewTypes.Application;
                    break;

                case Constants.DIALOG:
                    type = ViewTypes.Dialog;
                    break;

                case Constants.DOCUMENT:
                    type = ViewTypes.Document;
                    break;

                case Constants.TOOL:
                    type = ViewTypes.Tool;
                    break;

                default:
                    throw new ArgumentException(); // TODO
            }

            LoadContents(view);
        }

        public IList<IChildViewConfiguration> ChildViews => contents;

        public string Name => name;

        public ViewTypes Type => type;

        public IChildViewConfiguration GetChildViewConfiguration(string name) => contentsByName[name];

        private void LoadContents(View view)
        {
            if (view.Content != null)
            {
                contents.Add(new ContentConfiguration(view.Content));
            }
            else if (view.Contents != null)
            {
                foreach (var content in view.Contents)
                {
                    var configuration = new ContentConfiguration(content);
                    contentsByName.Add(configuration.Name, configuration);
                    contents.Add(configuration);
                }
            }
        }
    }
}
