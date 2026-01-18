namespace StarLab.Data.Import
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IImportProvider
    {
        /// <summary>
        /// Imports the data contained in an <see cref="IDataset"/> into the specified collection within a MongoDB database.
        /// </summary>
        /// <param name="source">An <see cref="IDataset"/> that contains the source data.</param>
        /// <param name="database">The name of the MongoDB database.</param>
        /// <param name="destination">The name of the destination collection.</param>
        void Import(IDataset source, string database, string destination);
    }
}
