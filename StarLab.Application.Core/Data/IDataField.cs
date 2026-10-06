using StarLab.Application.Data.Import;

namespace StarLab.Application.Data
{
    /// <summary>
    /// Represents a field from a dataset.
    /// </summary>
    public interface IDataField
    {
        /// <summary>
        /// Gets the index of the field.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the data type of the field.
        /// </summary>
        DataTypes DataType { get; }

        /// <summary>
        /// Gets the width of the field. If the field is of variable length, this property will return -1.
        /// </summary>
        int Width { get; }
    }
}
