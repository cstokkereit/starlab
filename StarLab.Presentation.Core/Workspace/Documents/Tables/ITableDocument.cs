namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// Represents a table document.
    /// </summary>
    public interface ITableDocument : IDocument
    {
        /// <summary>
        /// Gets the table.
        /// </summary>
        ITable Table { get; }
    }
}
