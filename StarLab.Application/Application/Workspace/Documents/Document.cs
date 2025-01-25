namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Represents the state of a document within the workspace hierarchy.
    /// </summary>
    internal class Document
    {
        private readonly string view; // The type name of the document view.

        private readonly string id; // The document ID.

        /// <summary>
        /// Initialises a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Document"/>.</param>
        /// <exception cref="ArgumentException"></exception>
        public Document(DocumentDTO dto)
        {
            Name = dto.Name ?? throw new ArgumentException();
            Path = dto.Path ?? throw new ArgumentException();

            view = dto.View ?? throw new ArgumentException();
            id = dto.ID ?? throw new ArgumentException();
        }

        /// <summary>
        /// Gets the document ID.
        /// </summary>
        public string ID => id;

        /// <summary>
        /// Gets or sets the document name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the document path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets the type name of the document view.
        /// </summary>
        public string View => view;
    }
}
