namespace StarLab.Application.Data.Import
{
    /// <summary>
    /// Defines methods that are specific to building an import definition that can be used to import data from a fixed width text file.
    /// </summary>
    public interface IFixedWidthImportDefinitionBuilder : IImportDefinitionBuilder
    {
        /// <summary>
        /// Adds a field in a fixed width text file to the import definition. 
        /// </summary>
        /// <param name="index">The field index.</param>
        /// <param name="name">The name that will be used to identify the field.</param>
        /// <param name="width">The number of characters used to represent the data in the field.</param>
        /// <param name="dataType">A <see cref="DataTypes"/> enum that specifies the field data type.</param>
        /// <returns>A reference to the <see cref="IFixedWidthImportDefinitionBuilder"/> that can be used to add other fields to the import definition.</returns>
        IFixedWidthImportDefinitionBuilder AddField(int index, string name, int width, DataTypes dataType);

        /// <summary>
        /// Prevents the data in a fixed width field from being imported. Required to calculate the start positions of subsequent data fields.
        /// </summary>
        /// <param name="index">The field index.</param>
        /// <param name="width">The number of characters used to represent the data in the field.</param>
        /// <returns>A reference to the <see cref="IFixedWidthImportDefinitionBuilder"/> that can be used to add other fields to the import definition.</returns>
        IFixedWidthImportDefinitionBuilder ExcludeField(int index, int width);
    }
}
