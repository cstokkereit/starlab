using StarLab.Application.Data.Import;
using Stratosoft.File.IO;

namespace StarLab.Data.Import
{
    /// <summary>
    /// A field containing composite data that is being imported from a file.
    /// </summary>
    internal sealed class FileBackedCompoundDataField : FileBackedDataField
    {
        private readonly int[] components; // An array containing the indices of the component fields.

        private readonly string format; // A format string that defines how the values in the component fields will be combined. 

        /// <summary>
        /// Initialises a new instance of the <see cref="FileBackedCompoundDataField"/> class.
        /// </summary>
        /// <param name="fieldDefinition">An <see cref="IFieldDefinition"/> that configures the <see cref="FileBackedCompoundDataField"/>.</param>
        /// <param name="parser">An <see cref="IFileParser"/> that will be used to read data from a file.</param>
        public FileBackedCompoundDataField(ICompoundFieldDefinition fieldDefinition, IFileParser parser)
            : base(fieldDefinition.Name, parser)
        { 
            components = fieldDefinition.Components;
            format = fieldDefinition.Format;
        }

        /// <summary>
        /// Gets the value of the field by combining the values of the component fields from the current row in the dataset.
        /// </summary>
        /// <returns>An <see cref="object"/> that holds the value of the field.</returns>
        protected override object GetValue()
        {
            var values = new string[components.Length];

            for (var n = 0; n < components.Length; n++)
            {
                values[n] = parser.GetValue(components[n]);
            }

            string value;

            if (string.IsNullOrEmpty(format))
            {
                value = string.Join(string.Empty, values);
            }
            else
            {
                value = string.Format(format, values);
            }

            return value;
        }
    }
}
