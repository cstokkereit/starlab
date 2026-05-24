namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// Represents a controller that can be used to control a table.
    /// </summary>
    public interface ITableController : IChildViewController
    {
        /// <summary>
        /// Updates the view with the new <see cref="ITable"/> definition following a change to the document or workspace.
        /// </summary>
        /// <param name="table">An <see cref="ITable"/> that specifies the state of the table.</param>
        void UpdateTable(ITable table);

        /// <summary>
        /// Reverts the preview to the old table settings.
        /// </summary>
        void UpdatePreview();
    }
}
