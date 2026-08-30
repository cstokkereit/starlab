using StarLab.Application.Workspace.Documents;

namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// View model representation of a table document in the workspace hierarchy.
    /// </summary>
    internal class TableDocument : Document, ITableDocument
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TableDocument"/>.
        /// </summary>
        /// <param name="dto">A <see cref="DocumentDTO"/> representation of the document.</param>
        public TableDocument(DocumentDTO dto)
            : base(dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Table = dto.Table != null ? new Table(dto.Table) : new Table();
        }

        /// <summary>
        /// Gets the table.
        /// </summary>
        public ITable Table { get; }
    }
}
