namespace StarLab.Application.Data.Import
{
    /// <summary>
    /// Represents a provider that can be used to import data into a database.
    /// </summary>
    public interface IDatabaseImportProvider
    {
        /// <summary>
        /// Imports the data contained in an <see cref="IDataset"/> into the specified table within a database.
        /// </summary>
        /// <param name="source">An <see cref="IForwardOnlyDataset"/> that contains the source data.</param>
        /// <param name="database">The name of the database.</param>
        /// <param name="destination">The name of the destination table.</param>
        void Import(IForwardOnlyDataset source, string database, string destination);
    }
}
