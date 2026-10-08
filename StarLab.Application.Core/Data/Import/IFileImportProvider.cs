namespace StarLab.Application.Data.Import
{
    /// <summary>
    /// Represents a provider that can be used to import data from a text file.
    /// </summary>
    public interface IFileImportProvider
    {
        /// <summary>
        /// Creates an <see cref="IDelimitedTextImportDefinitionBuilder"/> that can be used to construct an import definition for a delimited text file.
        /// </summary>
        /// <param name="delimiter">The delimiter that is used to separate fields in the source data file.</param>
        /// <param name="textDelimiter">The delimiter that is used to identify text in the source data file.</param>
        /// <returns>An <see cref="IDelimitedTextImportDefinitionBuilder"/> that can be used to construct an import definition for a delimited text file.</returns>
        IDelimitedTextImportDefinitionBuilder CreateImportDefinitionBuilder(string delimiter, string textDelimiter);

        /// <summary>
        /// Creates an <see cref="IDelimitedTextImportDefinitionBuilder"/> that can be used to construct an import definition for a delimited text file.
        /// </summary>
        /// <param name="delimiter">The delimiter that is used to separate fields in the source data file.</param>
        /// <returns>An <see cref="IDelimitedTextImportDefinitionBuilder"/> that can be used to construct an import definition for a delimited text file.</returns>
        IDelimitedTextImportDefinitionBuilder CreateImportDefinitionBuilder(string delimiter);

        /// <summary>
        /// Creates an <see cref="IFixedWidthImportDefinitionBuilder"/> that can be used to construct an import definition for a fixed width text file.
        /// </summary>
        /// <returns>An <see cref="IFixedWidthImportDefinitionBuilder"/> that can be used to construct an import definition for a fixed width text file.</returns>
        IFixedWidthImportDefinitionBuilder CreateImportDefinitionBuilder();

        /// <summary>
        /// Imports the data from the specified data file into an <see cref="IForwardOnlyDataset"/>.
        /// </summary>
        /// <param name="filename">The path to the data file.</param>
        /// <param name="importDefinition">An <see cref="IImportDefinition"/> that specifies the file format and identifies the fields that need to be imported.</param>
        /// <returns>An <see cref="IDataset"/> containing the imported data.</returns>
        IDataset ImportData(string filename, IImportDefinition importDefinition);

        /// <summary>
        /// Imports the data from the specified data file asynchronously into an <see cref="IForwardOnlyDataset"/>.
        /// </summary>
        /// <param name="filename">The path to the data file.</param>
        /// <param name="importDefinition">An <see cref="IImportDefinition"/> that specifies the file format and identifies the fields that need to be imported.</param>
        /// <returns>An <see cref="IDataset"/> containing the imported data.</returns>
        Task<IDataset> ImportDataAsync(string filename, IImportDefinition importDefinition);
    }
}
