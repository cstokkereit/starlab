using Stratosoft.Nomenclature.Properties;
using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature
{
    /// <summary>
    /// Defines a term from a <see cref="Nomenclature"/>.
    /// </summary>
    internal class Term : ITerm
    {
        private readonly IDictionary<string, IProperty> properties = new Dictionary<string, IProperty>(); // A dictionary containing the properties associated with this term indexed by property name.

        /// <summary>
        /// Initialises a new instance of the <see cref="Term"/> class.
        /// </summary>
        /// <param name="name">The name of the term.</param>
        /// <param name="description">A description of the term.</param>
        /// <exception cref="ArgumentException"></exception>
        public Term(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(Resources.NameNullOrEmpty, nameof(name));

            ID = Guid.NewGuid();

            Description = description;
            Name = name;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Term"/> class.
        /// </summary>
        /// <param name="name">The name of the term.</param>
        public Term(string name)
            : this(name, string.Empty) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Term"/> class.
        /// </summary>
        /// <param name="term">A POCO representation of the term.</param>
        /// <exception cref="ArgumentException"></exception>
        internal Term(XmlTerm term)
        {
            if (string.IsNullOrEmpty(term.Name)) throw new ArgumentException(Resources.NameNullOrEmpty, nameof(term));

            if (term.ID == Guid.Empty) throw new ArgumentException(Resources.IdRequired, nameof(term));

            Description = term.Description ?? string.Empty;
            Name = term.Name;
            ID = term.ID;

            AddProperties(term.Properties);
        }

        /// <summary>
        /// Gets the term description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the term  ID.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Gets the term name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the ID of the parent nomenclature. 
        /// </summary>
        public Guid NomenclatureID { get; private set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IProperty}"> containing the properties associated with this term.
        /// </summary>
        public IEnumerable<IProperty> Properties => properties.Values;

        /// <summary>
        /// Adds the <see cref="IProperty"/> provided to the term.
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/> being added.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Add(IProperty property)
        {
            if (properties.ContainsKey(property.Name)) throw new ArgumentException(string.Format(Resources.NamedPropertyAlreadyExists, property.Name), nameof(property));

            properties.Add(property.Name, property);

            if (property is Property p) p.Attach(this);
        }

        /// <summary>
        /// Gets the specified <see cref="IProperty"/>.
        /// </summary>
        /// <param name="name">The name of the required <see cref="IProperty"/>.</param>
        /// <returns>The specified <see cref="Property"/>.</returns>
        public IProperty GetProperty(string name)
        {
            return properties[name];
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="object"/>, which must also be a <see cref="Term"/> object, have the same value.
        /// </summary>
        /// <param name="other">The <see cref="Term"/> to compare to this instance.</param>
        /// <returns>true if other has the same value as this instance; false otherwise.</returns>
        public bool Equals(ITerm? other)
        {
            var result = other is not null;

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

        /// <summary>
        /// Determines whether this instance and a specified object have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns>true if obj is a <see cref="Term"/> and its value is the same as this instance; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return obj is ITerm property && Equals(property);
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
        /// Removes the <see cref="IProperty"/> with the specified name from the term.
        /// </summary>
        /// <param name="name">The name of the <see cref="IProperty"/> being removed.</param>
        public void Remove(string name)
        {
            if (properties.ContainsKey(name))
            {
                properties.Remove(name);
            }
        }

        /// <summary>
        /// Removes the specified <see cref="IProperty"/> from the term.
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/> being removed.</param>
        public void Remove(IProperty property)
        {
            Remove(property.Name);
        }

        /// <summary>
        /// Converts the value of the current <see cref="Term"/> object to its equivalent <see cref="string"/> representation.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of the current <see cref="Term"/> object.</returns>
        public override string ToString()
        {
            return Name + " " + ID.ToString();
        }

        /// <summary>
        /// Attaches this term to its parent <see cref="INomenclature"/>.
        /// </summary>
        /// <param name="term">The parent <see cref="INomenclature"/>.</param>
        /// <exception cref="InvalidOperationException"></exception>
        internal void Attach(INomenclature nomenclature)
        {
            if (NomenclatureID != Guid.Empty) throw new InvalidOperationException(Resources.TermAlreadyAttachedToParent);

            NomenclatureID = nomenclature.ID;
        }

        /// <summary>
        /// Adds the properties specified in the <see cref="IEnumerable{XmlProperty}"/> to the term.
        /// </summary>
        /// <param name="properties">An <see cref="IEnumerable{XmlProperty}"/> that defines the properties to be added.</param>
        private void AddProperties(IEnumerable<XmlProperty> properties)
        {
            foreach (XmlProperty property in properties)
            {
                Add(new Property(property));
            }
        }
    }
}
