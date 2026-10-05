using StarLab.Application.Data;
using StarLab.Application.Data.Import;

namespace StarLab.Data.Import
{
    /// <summary>
    /// A field containing data that is being imported from a file.
    /// </summary>
    internal class FileBackedDataField : IDataField
    {
        private readonly IFieldDefinition definition; // The field definition.

        /// <summary>
        /// Initialises a new instance of the <see cref="FileBackedDataField"/> class.
        /// </summary>
        /// <param name="definition">An <see cref="IFieldDefinition"/> that configures the <see cref="FileBackedDataField"/>.</param>
        public FileBackedDataField(IFieldDefinition definition)
        {
            ArgumentNullException.ThrowIfNull(definition, nameof(definition));

            this.definition = definition;
        }

        /// <summary>
        /// Gets the index of the field.
        /// </summary>
        public int Index => definition.Index;

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Name => definition.Name;

        /// <summary>
        /// Gets the data type of the field.
        /// </summary>
        public DataTypes DataType => definition.DataType;

        /// <summary>
        /// Gets the width of the field. If the field is of variable length, this property will return -1.
        /// </summary>
        public int Width => definition.Width;
    }
}
