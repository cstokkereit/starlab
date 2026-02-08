using Stratosoft.Nomenclature.Properties;

namespace Stratosoft.Nomenclature.Tests
{
    /// <summary>
    /// A class for constructing a <see cref="Term"/> that is part of a <see cref="Nomenclature">.
    /// </summary>
    public class TermBuilder
    {
        private readonly Dictionary<string, Property> properties = new(); // A dictionary that holds the properties of the term being constructed.

        /// <summary>
        /// Adds a <see cref="Property"/> to the <see cref="Term"/>.
        /// </summary>
        /// <param name="property">The <see cref="Property"/> being added to the term.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        /// <exception cref="ArgumentException"></exception>
        public TermBuilder AddProperty(Property property)
        {
            ArgumentNullException.ThrowIfNull(property, nameof(property));

            if (properties.ContainsKey(property.Name)) throw new ArgumentException(string.Format(Resources.NamedPropertyAlreadyExists, property.Name), nameof(property));

            properties.Add(property.Name, property);

            return this;
        }

        /// <summary>
        /// Adds a <see cref="Property"/> to the <see cref="Term"/>.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="description">A description of the term.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public TermBuilder AddProperty(string name, string description)
        {
            return AddProperty(new Property(name, description));
        }

        /// <summary>
        /// Adds a <see cref="Property"/> to the <see cref="Term"/>.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public TermBuilder AddProperty(string name)
        {
            return AddProperty(new Property(name));
        }

        /// <summary>
        /// Adds the properties in the <see cref="IEnumerable{Property}"/> provided to the <see cref="Term"/>.
        /// </summary>
        /// <param name="properties">An <see cref="IEnumerable{Property}"/> containing the properties being added to the term.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public TermBuilder AddProperties(IEnumerable<Property> properties)
        {
            ArgumentNullException.ThrowIfNull(properties, nameof(properties));

            foreach (Property property in properties)
            {
                AddProperty(property);
            }

            return this;
        }

        /// <summary>
        /// Creates the term.
        /// </summary>
        /// <param name="name">The name of the term.</param>
        /// <param name="description">A description of the term.</param>
        /// <returns>The specified <see cref="Term"/>.</returns>
        public Term CreateTerm(string name, string description)
        {
            Term term = new(name, description);
            AddProperties(term);
            return term;
        }

        /// <summary>
        /// Creates the term.
        /// </summary>
        /// <param name="name">The name of the term.</param>
        /// <returns>The specified <see cref="Term"/>.</returns>
        public Term CreateTerm(string name)
        {
            Term term = new(name);
            AddProperties(term);
            return term;
        }

        /// <summary>
        /// Adds the properties in the dictionary to the <see cref="Term"/> provided and clears the dictionary.
        /// </summary>
        /// <param name="term">The <see cref="Term"/> being constructed.</param>
        private void AddProperties(Term term)
        {
            foreach (Property property in properties.Values)
            {
                term.Add(property);
            }

            properties.Clear();
        }
    }
}
