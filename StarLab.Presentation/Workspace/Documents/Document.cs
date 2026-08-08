using StarLab.Application.Workspace.Documents;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// View model representation of a document in the workspace hierarchy.
    /// </summary>
    internal class Document : IDocument
    {
        private readonly string path; // The path to the folder containing the document.

        private readonly DocumentTypes type; // The document type.

        private readonly string view; // The name of the view config section.

        private readonly DocumentID id; // The document ID.

        private readonly string name; // The document name.

        /// <summary>
        /// Initialises a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="dto">A <see cref="DocumentDTO"/> representation of the document.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Document(DocumentDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            ArgumentException.ThrowIfNullOrEmpty(dto.Name, nameof(dto.Name)); // TODO - Validate dto and throw suitable exception (and elsewhere)
            ArgumentException.ThrowIfNullOrEmpty(dto.Path, nameof(dto.Path));
            ArgumentException.ThrowIfNullOrEmpty(dto.View, nameof(dto.View));
            ArgumentException.ThrowIfNullOrEmpty(dto.ID, nameof(dto.ID));

            id = new DocumentID(dto.ID);

            if (dto.Type != null) type = Enum.Parse<DocumentTypes>(dto.Type);
            
            name = dto.Name;
            path = dto.Path;
            view = dto.View;
        }

        /// <summary>
        /// Gets the document name including the path.
        /// </summary>
        public string FullName => $"{Path}/{Name}";

        /// <summary>
        /// Gets the document ID.
        /// </summary>
        public DocumentID ID => id;

        /// <summary>
        /// Gets the document name.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the path to the folder that contains the document.
        /// </summary>
        public string Path => path;

        /// <summary>
        /// Gets the document type.
        /// </summary>
        public DocumentTypes Type => type;

        /// <summary>
        /// Gets the name of the project that contains the document.
        /// </summary>
        public string Project
        {
            get
            { 
                var folders = Path.Split('/');
                return $"{folders[0]}/{folders[1]}";
            }
        }
        
        /// <summary>
        /// Gets the name of the view config section.
        /// </summary>
        public string View => view;
    }
}
