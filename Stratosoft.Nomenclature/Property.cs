using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature
{
    public class Property
    {
        #region Constructors

        public Property(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(null, nameof(name)); // TODO

            ID = Guid.NewGuid();

            Description = description;
            Name = name;
        }

        public Property(string name)
            : this(name, string.Empty) { }

        internal Property(XmlProperty property)
        {
            if (string.IsNullOrEmpty(property.Name)) throw new ArgumentException(null, nameof(property)); // TODO 

            if (property.ID == Guid.Empty) throw new ArgumentException(null, nameof(property)); // TODO

            Description = property.Description ?? string.Empty;
            Name = property.Name;
            ID = property.ID;
        }

        #endregion

        public string Description { get; }

        public Guid ID { get; }

        public string Name { get; }

        public Guid TermID { get; private set; }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="Property"/> object, have the same value.
        /// </summary>
        /// <param name="other">The <see cref="Property"/> to compare to this instance.</param>
        /// <returns>true if other has the same value as this instance; false otherwise.</returns>
        public bool Equals(Property? other)
        {
            var result = !ReferenceEquals(other, null);

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

        #region Object Overrides

        /// <summary>
        /// Determines whether this instance and a specified object have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns>true if obj is a <see cref="Property"/> and its value is the same as this instance; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Property && Equals((Property)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return 37 + ID.GetHashCode();
        }

        /// <summary>
        /// Converts the value of the current <see cref="Property"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="Property"/> object.</returns>
        public override string ToString()
        {
            return ID.ToString();
        }

        #endregion

        internal void Attach(Term term)
        {
            if (TermID != Guid.Empty) throw new InvalidOperationException(); // TODO
           
            TermID = term.ID;
        }
    }
}
