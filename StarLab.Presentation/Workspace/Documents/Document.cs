using StarLab.Application.Workspace.Documents;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// View model representation of a document in the workspace hierarchy.
    /// </summary>
    internal class Document : IDocument
    {
        private readonly string path; // The path to the folder containing the document.

        private readonly string view; // The name of the view config section.

        private readonly string id; // The document ID.

        private readonly string name; // The document name.

        /// <summary>
        /// Initialises a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="dto">A <see cref="DocumentDTO"/> representation of the document.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Document(DocumentDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            ArgumentException.ThrowIfNullOrEmpty(dto.Name, nameof(dto.Name));
            ArgumentException.ThrowIfNullOrEmpty(dto.Path, nameof(dto.Path));
            ArgumentException.ThrowIfNullOrEmpty(dto.View, nameof(dto.View));
            ArgumentException.ThrowIfNullOrEmpty(dto.ID, nameof(dto.ID));

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
