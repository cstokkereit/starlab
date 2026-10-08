using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using System.Data;
using System.Net.WebSockets;

namespace StarLab.Data.Import
{
    /// <summary>
    /// An implementation of the <see cref="IFileImportProvider"/> interface that can be used to import data from a text file.
    /// </summary>
    public class FileImportProvider : IFileImportProvider
    {
        /// <summary>
        /// Creates an <see cref="IDelimitedTextImportDefinitionBuilder"/> that can be used to construct an import definition for a delimited text file.
        /// </summary>
        /// <param name="delimiter">The delimiter that is used to separate fields in the source data file.</param>
        /// <param name="textDelimiter">The delimiter that is used to identify text in the source data file.</param>
        /// <returns>An <see cref="IDelimitedTextImportDefinitionBuilder"/> that can be used to construct an import definition for a delimited text file.</returns>
        public IDelimitedTextImportDefinitionBuilder CreateImportDefinitionBuilder(string delimiter, string textDelimiter)
        {
            return ImportDefinitionBuilder.GetInstance(delimiter, textDelimiter);
        }

        /// <summary>
        /// Creates an <see cref="IDelimitedTextImportDefinitionBuilder"/> that can be used to construct an import definition for a delimited text file.
        /// </summary>
        /// <param name="delimiter">The delimiter that is used to separate fields in the source data file.</param>
        /// <returns>An <see cref="IDelimitedTextImportDefinitionBuilder"/> that can be used to construct an import definition for a delimited text file.</returns>
        public IDelimitedTextImportDefinitionBuilder CreateImportDefinitionBuilder(string delimiter)
        {
            return ImportDefinitionBuilder.GetInstance(delimiter);
        }

        /// <summary>
        /// Creates an <see cref="IFixedWidthImportDefinitionBuilder"/> that can be used to construct an import definition for a fixed width text file.
        /// </summary>
        /// <returns>An <see cref="IFixedWidthImportDefinitionBuilder"/> that can be used to construct an import definition for a fixed width text file.</returns>
        public IFixedWidthImportDefinitionBuilder CreateImportDefinitionBuilder()
        {
            return ImportDefinitionBuilder.GetInstance();
        }

        /// <summary>
        /// Imports the data from the specified text file into an <see cref="IForwardOnlyDataset"/>.
        /// </summary>
        /// <param name="filename">The path to the text file.</param>
        /// <param name="importDefinition">An <see cref="IImportDefinition"/> that specifies the file format and identifies the fields that need to be imported.</param>
        /// <returns>An <see cref="IForwardOnlyDataset"/> containing the imported data.</returns>
        public IDataset ImportData(string filename, IImportDefinition importDefinition)
        {
            return new Dataset(new FileBackedDataset(filename, importDefinition));
        }

        /// <summary>
        /// Imports the data from the specified text file asynchronously into an <see cref="IForwardOnlyDataset"/>.
        /// </summary>
        /// <param name="filename">The path to the text file.</param>
        /// <param name="importDefinition">An <see cref="IImportDefinition"/> that specifies the file format and identifies the fields that need to be imported.</param>
        /// <returns>An <see cref="IForwardOnlyDataset"/> containing the imported data.</returns>
        public async Task<IDataset> ImportDataAsync(string filename, IImportDefinition importDefinition)
        {
            return await Task.Run(() => ImportData(filename, importDefinition));
        }
    }
}
