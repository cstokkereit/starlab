namespace StarLab.Application.Data.Import
{
    /// <summary>
    /// Defines methods that are specific to building an import definition that can be used to import data from a delimited text file.
    /// </summary>
    public interface IDelimitedTextImportDefinitionBuilder : IImportDefinitionBuilder
    {
        /// <summary>
        /// Adds a field in a delimited text file to the import definition. 
        /// </summary>
        /// <param name="index">The field index.</param>
        /// <param name="name">The name that will be used to identify the field.</param>
        /// <param name="dataType">A <see cref="DataTypes"/> enum that specifies the field data type.</param>
        /// <returns>A reference to the <see cref="IDelimitedTextImportDefinitionBuilder"/> that can be used to add other fields to the import definition.</returns>
        IDelimitedTextImportDefinitionBuilder AddField(int index, string name, DataTypes dataType);
    }
}
