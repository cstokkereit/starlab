namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Represents the state of a document within the workspace hierarchy.
    /// </summary>
    internal class Document
    {
        private readonly IFolder parent; // The folder that contains the document.

        private readonly string view; // The type name of the document view.

        private readonly string id; // The document ID.

        /// <summary>
        /// Initialises a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Document"/>.</param>
        /// <param name="parent">The <see cref="IFolder"/> that contains the document.</param>
        /// <exception cref="ArgumentException"></exception>
        public Document(DocumentDTO dto, IFolder parent)
        {
            id = string.IsNullOrEmpty(dto.ID) ? Guid.NewGuid().ToString() : dto.ID;

            this.parent = parent ?? throw new ArgumentException();

            Name = dto.Name ?? throw new ArgumentException();

            view = dto.View ?? throw new ArgumentException();
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
        /// Gets the document path.
        /// </summary>
        public string Path => parent.Path;

        /// <summary>
        /// Gets the type name of the document view.
        /// </summary>
        public string View => view;
    }
}
