using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// TODO
    /// </summary>
    internal struct DocumentDefinition : IDocumentDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        public DocumentDefinition(string name, string text, string description, DocumentTypes type, string image, string view)
        {
            Description = description;
            Image = image;
            Name = name;
            Text = text;
            Type = type;
            View = view;
        }

        public string Description { get; }

        public string Image { get; }

        public string Name { get; }

        public string Text { get; }

        public DocumentTypes Type { get; }

        public string View { get; }
    }
}
