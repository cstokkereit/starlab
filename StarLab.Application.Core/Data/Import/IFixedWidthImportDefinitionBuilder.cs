namespace StarLab.Application.Data.Import
{
    /// <summary>
    /// Defines methods that are specific to building an import definition that can be used to import data from a fixed width text file.
    /// </summary>
    public interface IFixedWidthImportDefinitionBuilder : IImportDefinitionBuilder
    {
        /// <summary>
        /// Adds a field to the import definition.
        /// </summary>
        /// <param name="index">The index of the source field.</param>
        /// <param name="name">The name that will be used to identify the field.</param>
        /// <param name="width">The width of the field.</param>
        /// <param name="dataType">An <see cref="DataTypes"/> that specifies the data type of the field.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        IFixedWidthImportDefinitionBuilder AddField(int index, string name, int width, DataTypes dataType);

        /// <summary>
        /// Prevents the data in a fixed width field from being imported. Required to calculate the start positions of subsequent data fields.
        /// </summary>
        /// <param name="index">The index of the source field.</param>
        /// <param name="width">The number of characters used to represent the data in the field.</param>
        /// <returns>A reference to the <see cref="IFixedWidthImportDefinitionBuilder"/> that can be used to add other fields to the import definition.</returns>
        IFixedWidthImportDefinitionBuilder ExcludeField(int index, int width);
    }
}
