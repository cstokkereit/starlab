using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature
{
    /// <summary>
    /// 
    /// </summary>
    public class Term
    {
        private readonly IDictionary<string, Property> propertiesByName = new Dictionary<string, Property>();

        private readonly List<Property> properties = new List<Property>();

        #region Constructors
        
        public Term(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(null, nameof(name)); // TODO 

            ID = Guid.NewGuid();

            Description = description;
            Name = name;
        }

        public Term(string name)
            : this(name, string.Empty) { }

        internal Term(XmlTerm term)
        {
            if (string.IsNullOrEmpty(term.Name)) throw new ArgumentException(null, nameof(term)); // TODO

            if (term.ID == Guid.Empty) throw new ArgumentException(null, nameof(term)); // TODO

            Description = term.Description ?? string.Empty;
            Name = term.Name;
            ID = term.ID;

            AddProperties(term.Properties);

            propertiesByName = properties.ToDictionary(p => p.Name, p => p);
        }

        #endregion

        public Property this[string name] { get => propertiesByName[name]; }

        public string Description { get; }

        public Guid ID { get; }

        public string Name { get; }

        public Guid NomenclatureID { get; private set; }

        public IReadOnlyList<Property> Properties { get => properties; }

        public void Add(Property property)
        {
            if (propertiesByName.ContainsKey(property.Name))
            {
                throw new ArgumentException();
            }
            else
            {
                propertiesByName.Add(property.Name, property);
                properties.Add(property);
                property.Attach(this);
            }
        }

        public void Remove(Property property)
        {
            Remove(property.Name);
        }

        public void Remove(string name)
        {
            if (!propertiesByName.ContainsKey(name))
            {
                throw new ArgumentException();
            }
            else
            {
                propertiesByName.Remove(name);
            }
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="Term"/> object, have the same value.
        /// </summary>
        /// <param name="other">The <see cref="Term"/> to compare to this instance.</param>
        /// <returns>true if other has the same value as this instance; false otherwise.</returns>
        public bool Equals(Term? other)
        {
            var result = !ReferenceEquals(other, null);

            if (result && other is Term property)
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
        /// <returns>true if obj is a <see cref="Term"/> and its value is the same as this instance; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Term && Equals((Term)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return 41 + ID.GetHashCode() * 19;
        }

        /// <summary>
        /// Converts the value of the current <see cref="Term"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="Term"/> object.</returns>
        public override string ToString()
        {
            return Name + " " + ID.ToString();
        }

        #endregion

        internal void Attach(Nomenclature nomenclature)
        {
            if (NomenclatureID != Guid.Empty) throw new InvalidOperationException(); // TODO

            NomenclatureID = nomenclature.ID;
        }

        private void AddProperties(IEnumerable<XmlProperty> properties)
        {
            foreach (XmlProperty property in properties)
            {
                Add(new Property(property));
            }
        }
    }
}
