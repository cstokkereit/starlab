using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature
{
    public class Nomenclature
    {
        private readonly IDictionary<string, Term> termsByName = new Dictionary<string, Term>();

        #region Constructors

        public Nomenclature(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(null, nameof(name)); // TODO 

            ID = Guid.NewGuid();

            Description = description;
            Name = name;
        }

        public Nomenclature(string name)
            : this(name, string.Empty) { }

        internal Nomenclature(XmlNomenclature nomenclature)
        {
            if (string.IsNullOrEmpty(nomenclature.Name)) throw new ArgumentException(null, nameof(nomenclature)); // TODO 

            if (nomenclature.ID == Guid.Empty) throw new ArgumentException(null, nameof(nomenclature)); // TODO

            Description = nomenclature.Description ?? string.Empty;
            Name = nomenclature.Name;
            ID = nomenclature.ID;

            AddTerms(nomenclature.Terms);
        }

        #endregion

        public string Description { get; }

        public Guid ID { get; }

        public string Name { get; }

        public IReadOnlyDictionary<string, Term> Terms => (IReadOnlyDictionary<string, Term>)termsByName;

        public void Add(Term term)
        {
            if (termsByName.ContainsKey(term.Name))
            {
                throw new ArgumentException();
            }
            else
            {
                termsByName.Add(term.Name, term);
                term.Attach(this);
            }
        }

        public void Remove(string name)
        {
            if (termsByName.ContainsKey(name))
            {
                termsByName.Remove(name);
            }
        }

        public void Remove(Term term)
        {
            Remove(term.Name);
        }

        private void AddTerms(IEnumerable<XmlTerm> terms)
        {
            foreach (XmlTerm term in terms)
            {
                Add(new Term(term));
            }
        }
    }
}
