using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Application.Workspace.Documents.Tables;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Application model represention of a document.
    /// </summary>
    internal class Document
    {
        private readonly DocumentID id; // The document ID.

        private readonly string type; // The document type.

        private readonly string view; // The type name of the document view.

        private IFolder folder; // The folder that contains the document.

        /// <summary>
        /// Initialises a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Document"/>.</param>
        /// <param name="folder">The <see cref="IFolder"/> that contains the document.</param>
        /// <exception cref="ArgumentException"></exception>
        public Document(DocumentDTO dto, IFolder folder)
        {
            this.folder = folder ?? throw new ArgumentNullException(nameof(folder));

            id = string.IsNullOrEmpty(dto.ID) ? new DocumentID() : new DocumentID(dto.ID);

            Chart = dto.Chart == null ? null : new Chart(dto.Chart);
            Table = dto.Table == null ? null : new Table(dto.Table);

            Name = dto.Name;

            type = dto.Type;
            view = dto.View;
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

            this.folder = folder ?? throw new ArgumentNullException(nameof(folder));

            id = new DocumentID();

            Chart = document.Chart;
            Table = document.Table;

            Name = name;

            type = document.Type;
            view = document.View;
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

            this.folder = folder ?? throw new ArgumentNullException(nameof(folder));

            id = new DocumentID();

            Chart = document.Chart;
            Table = document.Table;
            Name = document.Name;

            type = document.Type;
            view = document.View;
        }

        /// <summary>
        /// Gets or sets the chart.
        /// </summary>
        public Chart? Chart { get; set; }

        /// <summary>
        /// Gets the document ID.
        /// </summary>
        public DocumentID ID => id;

        /// <summary>
        /// Gets or sets the document name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the document path.
        /// </summary>
        public string Path => folder.Path;

        /// <summary>
        /// Gets the name of the project that contains the document.
        /// </summary>
        public string Project => Path.Split('/')[1];

        /// <summary>
        /// Gets or sets the table.
        /// </summary>
        public Table? Table { get; set; }

        /// <summary>
        /// Gets the document type.
        /// </summary>
        public string Type => type;

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
