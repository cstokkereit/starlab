using StarLab.Application.Data.Import;

namespace StarLab.Data.Import
{
    /// <summary>
    /// Defines a field containing values that can be imported.
    /// </summary>
    internal class FieldDefinition : IFieldDefinition
    {
        const int DEFAULT_WIDTH = -1; // A value indicating that the number of characters that comprise the field has not been specified.

        private readonly DataTypes dataType = DataTypes.Text; // The data type of the field. 

        private readonly string name = string.Empty; // The name of the field.

        private readonly bool include;  // A flag inidicating whether the field is to be imported or not.

        private readonly int index; // The index of the field.

        private readonly int width = DEFAULT_WIDTH; // The number of characters that comprise the field.

        /// <summary>
        /// Initialises a new instance of the <see cref="FieldDefinition"/> class.
        /// </summary>
        /// <param name="index">The index of the field.</param>
        /// <param name="name">The name of the field.</param>
        /// <param name="width">The number of characters that comprise the field.</param>
        /// <param name="dataType">A <see cref="DataTypes"/> enum that specifies the data type.</param>
        public FieldDefinition(int index, string name, int width, DataTypes dataType)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));

            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            this.dataType = dataType;
            this.index = index;
            this.width = width;
            this.name = name;

            include = true;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="FieldDefinition"/> class.
        /// </summary>
        /// <param name="index">The index of the field.</param>
        /// <param name="name">The name of the field.</param>
        /// <param name="dataType">A <see cref="DataTypes"/> enum that specifies the data type.</param>
        public FieldDefinition(int index, string name, DataTypes dataType)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));

            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            this.dataType = dataType;
            this.index = index;
            this.name = name;

            include = true;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="FieldDefinition"/> class.
        /// </summary>
        /// <param name="index">The index of the field.</param>
        /// <param name="width">The number of characters that comprise the field.</param>
        public FieldDefinition(int index, int width)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));

            this.index = index;
            this.width = width;

            include = false;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="FieldDefinition"/> class.
        /// </summary>
        /// <param name="index">The index of the field.</param>
        public FieldDefinition(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));

            this.index = index;

            include = false;
        }

        /// <summary>
        /// Returns a <see cref="DataTypes"/> value that specifies the data type of the field.
        /// </summary>
        public DataTypes DataType => dataType;

        /// <summary>
        /// Returns <see cref="true"/> if the field is to be imported; <see cref="false"/> otherwise.
        /// </summary>
        public bool Include => include;

        /// <summary>
        /// Gets the index of the field.
        /// </summary>
        public int Index => index;

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the number of characters that comprise the field.
        /// </summary>
        public int Width => width;
    }
}
