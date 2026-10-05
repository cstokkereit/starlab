using StarLab.Application.Data.Import;
using System.Diagnostics;

namespace StarLab.Data.Import
{
    /// <summary>
    /// A class for constructing an <see cref="ImportDefinition"/> that specifies which fields will be imported into a database.
    /// </summary>
    public class ImportDefinitionBuilder : IDelimitedTextImportDefinitionBuilder, IFixedWidthImportDefinitionBuilder
    {
        private ImportDefinition? importDefinition = null; // The import definition being constructed.

        /// <summary>
        /// Initiaslises a new instance of the <see cref="ImportDefinitionBuilder"/> class.
        /// </summary>
        /// <param name="fileType">A <see cref="FileTypes"/> that specifies the format of the source data file.</param>
        /// <param name="delimiter">The delimiter that is used to separate fields in the source data file.</param>
        /// <param name="textDelimiter">The delimiter that is used to identify text in the source data file.</param>
        private ImportDefinitionBuilder(FileTypes fileType, string delimiter, string textDelimiter)
        {
            importDefinition = new ImportDefinition(fileType, delimiter, textDelimiter);
        }

        /// <summary>
        /// Initiaslises a new instance of the <see cref="ImportDefinitionBuilder"/> class.
        /// </summary>
        /// <param name="fileType">A <see cref="FileTypes"/> that specifies the format of the source data file.</param>
        private ImportDefinitionBuilder(FileTypes fileType)
        {
            importDefinition = new ImportDefinition(fileType);
        }

        /// <summary>
        /// Creates an <see cref="IDelimitedTextImportDefinitionBuilder"/>.
        /// </summary>
        /// <param name="delimiter">The delimiter that is used to separate fields in the source data file.</param>
        /// <param name="textDelimiter">The delimiter that is used to identify text in the source data file.</param>
        /// <returns>An <see cref="IDelimitedTextImportDefinitionBuilder"/> that can be used to construct an import definition for a delimited text file.</returns>
        public static IDelimitedTextImportDefinitionBuilder GetInstance(string delimiter, string textDelimiter)
        {
            return new ImportDefinitionBuilder(FileTypes.DelimitedText, delimiter, textDelimiter);
        }

        /// <summary>
        /// Creates an <see cref="IDelimitedTextImportDefinitionBuilder"/>.
        /// </summary>
        /// <param name="delimiter">The delimiter that is used to separate fields in the source data file.</param>
        /// <returns>An <see cref="IDelimitedTextImportDefinitionBuilder"/> that can be used to construct an import definition for a delimited text file.</returns>
        public static IDelimitedTextImportDefinitionBuilder GetInstance(string delimiter)
        {
            return new ImportDefinitionBuilder(FileTypes.DelimitedText, delimiter, string.Empty);
        }

        /// <summary>
        /// Creates an <see cref="IFixedWidthImportDefinitionBuilder"/>.
        /// </summary>
        /// <returns>An <see cref="IFixedWidthImportDefinitionBuilder"/> that can be used to construct an import definition for a fixed width text file.</returns>
        public static IFixedWidthImportDefinitionBuilder GetInstance()
        {
            return new ImportDefinitionBuilder(FileTypes.FixedWidthText);
        }

        /// <summary>
        /// Adds a compound field to the import definition.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="format">A format <see cref="string"/> that defines how the values in the component fields will be combined.</param>
        /// <param name="components">The indices of the component fields.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public IImportDefinitionBuilder AddCompoundField(string name, string format, int[] components)
        {
            importDefinition?.AddCompoundField(name, format, components);

            return this;
        }

        /// <summary>
        /// Adds a compound field to the import definition.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="components">The indices of the source fields.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public IImportDefinitionBuilder AddCompoundField(string name, int[] components)
        {
            importDefinition?.AddCompoundField(name, components);

            return this;
        }

        /// <summary>
        /// Adds a field to the import definition.
        /// </summary>
        /// <param name="index">The index of the source field.</param>
        /// <param name="name">The name that will be used to identify the field.</param>
        /// <param name="dataType">An <see cref="DataTypes"/> that specifies the data type of the field.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public IDelimitedTextImportDefinitionBuilder AddField(int index, string name, DataTypes dataType)
        {
            importDefinition?.AddField(index, name, dataType);

            return this;
        }

        /// <summary>
        /// Adds a field to the import definition.
        /// </summary>
        /// <param name="index">The index of the source field.</param>
        /// <param name="name">The name that will be used to identify the field.</param>
        /// <param name="width">The width of the field.</param>
        /// <param name="dataType">An <see cref="DataTypes"/> that specifies the data type of the field.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public IFixedWidthImportDefinitionBuilder AddField(int index, string name, int width, DataTypes dataType)
        {
            importDefinition?.AddField(index, name, width, dataType);

            return this;
        }

        /// <summary>
        /// Specifies the number of header rows that the data file contains. The header rows will be skipped when the data is imported.
        /// </summary>
        /// <param name="rows">The number of header rows.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public IImportDefinitionBuilder AddHeaderRows(int rows)
        {
            importDefinition?.AddHeaderRows(rows);

            return this;
        }

        /// <summary>
        /// Builds the import definition and resets the builder so that it can be used to construct another import definition.
        /// </summary>
        /// <returns>The specified <see cref="IImportDefinition"/>.</returns>
        public IImportDefinition Build()
        {
            Debug.Assert(importDefinition != null);

            var retval = importDefinition;

            importDefinition = new ImportDefinition(retval.FileType, retval.Delimiter, retval.TextDelimiter);

            return retval;
        }

        /// <summary>
        /// Excludes a field from the import definition.
        /// </summary>
        /// <param name="index">The index of the source field.</param>
        /// <param name="width">The width of the field.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public IFixedWidthImportDefinitionBuilder ExcludeField(int index, int width)
        {
            importDefinition?.ExcludeField(index, width);

            return this;
        }
    }
}
