using Stratosoft.Nomenclature.Properties;
using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature
{
    /// <summary>
    /// Defines a nomenclature.
    /// </summary>
    internal class Nomenclature : INomenclature
    {
        private readonly IDictionary<string, ITerm> terms = new Dictionary<string, ITerm>(); // A dictionary containing the terms associated with this nomenclature indexed by term name.

        /// <summary>
        /// Initialises a new instance of the <see cref="Nomenclature"/> class.
        /// </summary>
        /// <param name="name">The name of the nomenclature.</param>
        /// <param name="description">A description of the nomenclature.</param>
        public Nomenclature(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(Resources.NameNullOrEmpty, nameof(name));

            ID = Guid.NewGuid();

            Description = description;
            Name = name;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Nomenclature"/> class.
        /// </summary>
        /// <param name="name">The name of the nomenclature.</param>
        public Nomenclature(string name)
            : this(name, string.Empty) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Nomenclature"/> class.
        /// </summary>
        /// <param name="nomenclature">A POCO representation of the nomenclature.</param>
        /// <exception cref="ArgumentException"></exception>
        internal Nomenclature(XmlNomenclature nomenclature)
        {
            if (string.IsNullOrEmpty(nomenclature.Name)) throw new ArgumentException(Resources.NameNullOrEmpty, nameof(nomenclature));

            if (nomenclature.ID == Guid.Empty) throw new ArgumentException(Resources.IdRequired, nameof(nomenclature));

            Description = nomenclature.Description ?? string.Empty;
            Name = nomenclature.Name;
            ID = nomenclature.ID;

            AddTerms(nomenclature.Terms);
        }

        /// <summary>
        /// Gets the nomenclature description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the nomenclature ID.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Gets the nomenclature name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{Term}"> containing the terms associated with this nomenclature.
        /// </summary>
        public IEnumerable<ITerm> Terms => terms.Values;

        /// <summary>
        /// Adds the <see cref="Term"/> provided to the nomenclature.
        /// </summary>
        /// <param name="term">The <see cref="Term"/> being added.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Add(ITerm term)
        {
            if (terms.ContainsKey(term.Name)) throw new ArgumentException(string.Format(Resources.NamedTermAlreadyExists, term.Name), nameof(term));

            terms.Add(term.Name, term);

            if (term is Term t) t.Attach(this);
        }

        /// <summary>
        /// Gets the specified <see cref="ITerm"/>.
        /// </summary>
        /// <param name="name">The name of the required <see cref="ITerm"/>.</param>
        /// <returns>The specified <see cref="ITerm"/>.</returns>
        public ITerm GetTerm(string name)
        {
            return terms[name];
        }

        /// <summary>
        /// Removes the <see cref="Term"/> with the specified name from the nomenclature.
        /// </summary>
        /// <param name="name">The name of the <see cref="Term"/> being removed.</param>
        public void Remove(string name)
        {
            if (terms.ContainsKey(name))
            {
                terms.Remove(name);
            }
        }

        /// <summary>
        /// Removes the specified <see cref="ITerm"/> from the nomenclature.
        /// </summary>
        /// <param name="term">The <see cref="ITerm"/> being removed.</param>
        public void Remove(ITerm term)
        {
            Remove(term.Name);
        }

        /// <summary>
        /// Adds the terms specified in the <see cref="IEnumerable{XmlTerm}"/> to the nomenclature.
        /// </summary>
        /// <param name="terms">An <see cref="IEnumerable{XmlTerm}"/> that defines the terms to be added.</param>
        private void AddTerms(IEnumerable<XmlTerm> terms)
        {
            foreach (XmlTerm term in terms)
            {
                Add(new Term(term));
            }
        }
    }
}
