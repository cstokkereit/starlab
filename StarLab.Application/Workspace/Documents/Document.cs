namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Represents the state of a document within the workspace hierarchy.
    /// </summary>
    internal class Document
    {
        private readonly string view; // The type name of the document view.

        private readonly string id; // The document ID.

        private IFolder folder; // The folder that contains the document.

        /// <summary>
        /// Initialises a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Document"/>.</param>
        /// <param name="folder">The <see cref="IFolder"/> that contains the document.</param>
        /// <exception cref="ArgumentException"></exception>
        public Document(DocumentDTO dto, IFolder folder)
        {
            id = string.IsNullOrEmpty(dto.ID) ? Guid.NewGuid().ToString() : dto.ID;

            this.folder = folder ?? throw new ArgumentException();

            Name = dto.Name ?? throw new ArgumentException();

            view = dto.View ?? throw new ArgumentException();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="document">The document being copied.</param>
        /// <param name="name">The document name.</param>
        /// <param name="folder">The <see cref="IFolder"/> that contains the document.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Document(Document document, string name, IFolder folder)
        {
            ArgumentNullException.ThrowIfNull(document, nameof(document));

            this.folder = folder ?? throw new ArgumentNullException();

            id = Guid.NewGuid().ToString();

            view = document.view;

            Name = name;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="document">The document being copied.</param>
        /// <param name="folder">The <see cref="IFolder"/> that contains the document.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Document(Document document, IFolder folder)
        {
            ArgumentNullException.ThrowIfNull(document, nameof(document));

            this.folder = folder ?? throw new ArgumentNullException();

            id = Guid.NewGuid().ToString();

            Name = document.Name;

            view = document.view;
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
        public string Path => folder.Path;

        /// <summary>
        /// Gets the type name of the document view.
        /// </summary>
        public string View => view;

        /// <summary>
        /// Sets the parent folder.
        /// </summary>
        /// <param name="folder">The new parent <see cref="IFolder"/>.</param>
        public void SetFolder(IFolder folder) { this.folder = folder; }
    }
}
