using Stratosoft.Nomenclature.Properties;
using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature
{
    /// <summary>
    /// A property of an <see cref="ITerm">.
    /// </summary>
    internal class Property : IProperty
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="description">A description of the property.</param>
        /// <exception cref="ArgumentException"></exception>
        public Property(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(Resources.NameNullOrEmpty, nameof(name));

            ID = Guid.NewGuid();

            Description = description;
            Name = name;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public Property(string name)
            : this(name, string.Empty) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="property">A POCO representation of the property.</param>
        /// <exception cref="ArgumentException"></exception>
        internal Property(XmlProperty property)
        {
            if (string.IsNullOrEmpty(property.Name)) throw new ArgumentException(Resources.NameNullOrEmpty, nameof(property));

            if (property.ID == Guid.Empty) throw new ArgumentException(Resources.IdRequired, nameof(property));

            Description = property.Description ?? string.Empty;
            Name = property.Name;
            ID = property.ID;
        }

        /// <summary>
        /// Gets the property description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the property ID.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Gets the property name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the ID of the parent term.
        /// </summary>
        public Guid TermID { get; private set; }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="object"/>, which must also be a <see cref="Property"/> object, have the same value.
        /// </summary>
        /// <param name="other">The <see cref="Property"/> to compare to this instance.</param>
        /// <returns>true if other has the same value as this instance; false otherwise.</returns>
        public bool Equals(IProperty? other)
        {
            var result = other is not null;

            if (result && other is Property property)
            {
                result &= property.ID == ID;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Determines whether this instance and a specified object have the same value.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare to this instance.</param>
        /// <returns>true if obj is a <see cref="Property"/> and its value is the same as this instance; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return obj is IProperty property && Equals(property);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Name);
        }

        /// <summary>
        /// Converts the value of the current <see cref="Property"/> object to its equivalent <see cref="string"/> representation.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of the current <see cref="Property"/> object.</returns>
        public override string ToString()
        {
            return $"{Name} ({ID})";
        }

        /// <summary>
        /// Attaches this property to its parent <see cref="Term"/>.
        /// </summary>
        /// <param name="term">The parent <see cref="Term"/>.</param>
        /// <exception cref="InvalidOperationException"></exception>
        internal void Attach(Term term)
        {
            if (TermID != Guid.Empty) throw new InvalidOperationException(Resources.PropertyAlreadyAttachedToParent);
           
            TermID = term.ID;
        }
    }
}
