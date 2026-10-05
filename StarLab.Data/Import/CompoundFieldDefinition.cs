using StarLab.Application.Data.Import;
using StarLab.Shared;

namespace StarLab.Data.Import
{
    /// <summary>
    /// Defines a field containing values that are composed of values from other fields.
    /// </summary>
    internal class CompoundFieldDefinition : FieldDefinition, ICompoundFieldDefinition
    {
        private readonly int[] components; // The indices of the component fields.

        private readonly string format; // A format string that defines how the values in the component fields will be combined.

        /// <summary>
        /// Initialises a new instance of the <see cref="CompoundFieldDefinition"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="format">A format <see cref="string"/> that defines how the values in the component fields will be combined.</param>
        /// <param name="components">The indices of the component fields.</param>
        public CompoundFieldDefinition(string name, string format, int[] components)
            : base(name, DataTypes.Text)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(format, nameof(format));

            if (components.Length == 0) throw new ArgumentException(ExceptionMessages.ComponentFieldIndicesRequired, nameof(components));

            this.components = components;
            this.format = format;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="CompoundFieldDefinition"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="components">The indices of the component fields.</param>
        public CompoundFieldDefinition(string name, int[] components)
            : base(name, DataTypes.Text) 
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            if (components.Length == 0) throw new ArgumentException(ExceptionMessages.ComponentFieldIndicesRequired, nameof(components));

            this.components = components;

            format = string.Empty;
        }

        /// <summary>
        /// Gets an <see cref="int[]"/> containing the indices of the component fields.
        /// </summary>
        public int[] Components => components;

        /// <summary>
        /// Gets the format <see cref="string"/> that defines how the values in the component fields will be combined.
        /// </summary>
        public string Format => format;
    }
}
