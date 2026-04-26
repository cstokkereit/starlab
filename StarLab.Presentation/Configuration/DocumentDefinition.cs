using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// The definition of a document that can be added to the workspace. This class is immutable and is used to provide the information necessary for creating a document and displaying it in the UI.
    /// </summary>
    internal readonly struct DocumentDefinition : IDocumentDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DocumentDefinition"> struct.
        /// </summary>
        /// <param name="name">The name of the document type.</param>
        /// <param name="displayName">The display name of the document type.</param>
        /// <param name="description">A description of the document type.</param>
        /// <param name="type">The document type.</param>
        /// <param name="image">The name of the image that represents the document type.</param>
        /// <param name="view">The name of the view that is used to display the document.</param>
        public DocumentDefinition(string name, string displayName, string description, DocumentTypes type, string image, string view)
        {
            Description = description;
            DisplayName = displayName;
            Image = image;
            Name = name;
            Type = type;
            View = view;
        }

        /// <summary>
        /// Gets a description of the document type.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the display name of the document type.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Gets the name of the image that represents the document type.
        /// </summary>
        public string Image { get; }

        /// <summary>
        /// Gets the name of the document type.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the document type.
        /// </summary>
        public DocumentTypes Type { get; }

        /// <summary>
        /// Gets the name of the view that is used to display the document.
        /// </summary>
        public string View { get; }
    }
}
