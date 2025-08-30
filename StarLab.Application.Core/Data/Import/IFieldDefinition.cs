namespace StarLab.Application.Data.Import
{
    /// <summary>
    /// The definition of a field that can be imported.
    /// </summary>
    public interface IFieldDefinition
    {
        /// <summary>
        /// Returns a <see cref="DataTypes"/> value that specifies the data type of the field.
        /// </summary>
        DataTypes DataType { get; }

        /// <summary>
        /// Returns <see cref="true"/> if the field is to be imported; <see cref="false"/> otherwise.
        /// </summary>
        bool Include { get; }

        /// <summary>
        /// Gets the index of the field.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the number of characters that comprise the field.
        /// </summary>
        int Width { get; }
    }
}
