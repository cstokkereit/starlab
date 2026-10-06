namespace StarLab.Application.Data.Import
{
    /// <summary>
    /// Defines methods that are specific to building an import definition that can be used to import data from a delimited text file.
    /// </summary>
    public interface IDelimitedTextImportDefinitionBuilder : IImportDefinitionBuilder
    {
        /// <summary>
        /// Adds a field to the import definition.
        /// </summary>
        /// <param name="index">The index of the source field.</param>
        /// <param name="name">The name that will be used to identify the field.</param>
        /// <param name="dataType">An <see cref="DataTypes"/> that specifies the data type of the field.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        IDelimitedTextImportDefinitionBuilder AddField(int index, string name, DataTypes dataType);
    }
}
