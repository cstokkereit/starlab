namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Represents a document in the workspace hierarchy.
    /// </summary>
    internal class Document : IDocument
    {
        private readonly string path; // The path to the folder containing the document.

        private readonly string view; // The name of the view config section.

        private readonly string id; // The document ID.

        private string name; // The document name.

        /// <summary>
        /// Initialises a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="id">The document ID.</param>
        /// <param name="name">The name of the document.</param>
        /// <param name="path">The path to the folder containing the document.</param>
        /// <param name="view">The name of the view config section.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Document(string id, string name, string path, string? view)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(view)) throw new ArgumentNullException(nameof(view));
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            this.name = name;
            this.path = path;
            this.view = view;
            this.id = id;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="name">The name of the document.</param>
        /// <param name="path">The path to the folder containing the document.</param>
        /// <param name="view">The name of the view config section.</param>
        public Document(string name, string path, string view)                                                                                
            : this(Guid.NewGuid().ToString(), name, path, view) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="dto">A <see cref="DocumentDTO"/> representation of the document.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Document(DocumentDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException(nameof(dto.Name));
            if (string.IsNullOrEmpty(dto.Path)) throw new ArgumentException(nameof(dto.Path));
            if (string.IsNullOrEmpty(dto.View)) throw new ArgumentException(nameof(dto.View));
            if (string.IsNullOrEmpty(dto.ID)) throw new ArgumentException(nameof(dto.ID));

            name = dto.Name;
            path = dto.Path;
            view = dto.View;
            id = dto.ID;
        }

        /// <summary>
        /// Gets the document name including the path.
        /// </summary>
        public string FullName => $"{Path}/{Name}";

        /// <summary>
        /// Gets the document ID.
        /// </summary>
        public string ID => id;

        /// <summary>
        /// Gets the document name.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the path to the folder that contains the document.
        /// </summary>
        public string Path => path;

        /// <summary>
        /// Gets the name of the view config section.
        /// </summary>
        public string View => view;
    }
}
