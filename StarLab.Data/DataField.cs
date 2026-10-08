using StarLab.Application.Data;
using StarLab.Application.Data.Import;

namespace StarLab.Data
{
    /// <summary>
    /// A field from a dataset.
    /// </summary>
    internal class DataField : IDataField
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DataField"/> class.
        /// </summary>
        /// <param name="definition">An <see cref="IFieldDefinition"/> that configures the <see cref="DataField"/>.</param>
        public DataField(IFieldDefinition definition)
        {
            ArgumentNullException.ThrowIfNull(definition, nameof(definition));

            DataType = definition.DataType;
            Index = definition.Index;
            Width = definition.Width;
            Name = definition.Name;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DataField"/> class.
        /// </summary>
        /// <param name="field">An <see cref="IDataField"/> that is being copied.</param>
        public DataField(IDataField field)
        {
            DataType = field.DataType;
            Index = field.Index;
            Width = field.Width;
            Name = field.Name;
        }

        /// <summary>
        /// Gets the index of the field.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the data type of the field.
        /// </summary>
        public DataTypes DataType { get; }

        /// <summary>
        /// Gets the width of the field. If the field is of variable length, this property will return -1.
        /// </summary>
        public int Width { get; }
    }
}
