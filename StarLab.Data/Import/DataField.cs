using StarLab.Application.Data.Import;
using Stratosoft.File.IO;

namespace StarLab.Data.Import
{
    /// <summary>
    /// A field containing data that is being imported from a file.
    /// </summary>
    public class DataField : IDataField
    {
        protected readonly IFileParser parser; //  The file parser that will be used to read data from a file.

        private readonly IConverter converter; // Converts the string value read in by the parser to the data type specified in the import definition.

        private readonly string name; // The name of the field.

        /// <summary>
        /// Initialises a new instance of the <see cref="DataField"/> class.
        /// </summary>
        /// <param name="fieldDefinition">An <see cref="IFieldDefinition"/> that configures the <see cref="DataField"/>.</param>
        /// <param name="parser">An <see cref="IFileParser"/> that will be used to read data from a file.</param>
        public DataField(IFieldDefinition fieldDefinition, IFileParser parser)
        {
            ArgumentException.ThrowIfNullOrEmpty(fieldDefinition.Name, nameof(fieldDefinition.Name)); // TODO
            ArgumentNullException.ThrowIfNull(parser, nameof(parser));

            converter = Converters.GetConverter(fieldDefinition.DataType);

            name = fieldDefinition.Name;

            this.parser = parser;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DataField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="parser">An <see cref="IFileParser"/> that will be used to read data from a file.</param>
        protected DataField(string name, IFileParser parser)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name)); // TODO
            ArgumentNullException.ThrowIfNull(parser, nameof(parser));

            converter = Converters.GetConverter(DataTypes.Text);

            this.parser = parser;
            this.name = name;
        }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Name { get => name; }

        /// <summary>
        /// Gets the value of the field from the current row in the dataset.
        /// </summary>
        public object Value { get => GetValue(); }

        /// <summary>
        /// Gets the value of the field from the current row in the dataset.
        /// </summary>
        /// <returns>An <see cref="object"/> that holds the value of field with the specified data type.</returns>
        protected virtual object GetValue()
        {
            var value = parser.GetValue(Name);

            if (string.IsNullOrEmpty(value)) return string.Empty; // TODO - If field is required throw an exception

            return converter.Convert(value);
        }
    }
}
