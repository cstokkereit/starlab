namespace StarLab.Application.Data.Import
{
    /// <summary>
    /// An interface that provides the information required to import data from a data file into the application.
    /// </summary>
    public interface IImportDefinition
    {
        /// <summary>
        /// Gets an <see cref="IReadOnlyList{ICompoundFieldDefinition}"/> containing the compound field definitions.
        /// </summary>
        IReadOnlyList<ICompoundFieldDefinition> CompoundFields { get; }

        /// <summary>
        /// Gets an <see cref="IReadOnlyList{IFieldDefinition}"/> containing the field definitions.
        /// </summary>
        IReadOnlyList<IFieldDefinition> Fields { get; }

        /// <summary>
        /// Specifies the delimiter used to separate fields in a delimited text file.
        /// </summary>
        string Delimiter { get; }

        /// <summary>
        /// Specifies the type of data file being imported.
        /// </summary>
        FileTypes FileType {  get; }

        /// <summary>
        /// Gets the name of the import definition.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Specifies the delimiter used to identify text fields in a delimited text file.
        /// </summary>
        string TextDelimiter { get; }
    }
}
