namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="ITableViewPresenter"/> to control the behaviour of a table.
    /// </summary>
    public interface ITableView : IChildView
    {
        /// <summary>
        /// Updates the state of the table following a change.
        /// </summary>
        /// <param name="table">An <see cref="ITable"/> that specifies the new state of the table.</param>
        void UpdateTable(ITable table);
    }
}
