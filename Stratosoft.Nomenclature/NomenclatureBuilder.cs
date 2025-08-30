namespace Stratosoft.Nomenclature
{
    public class NomenclatureBuilder
    {
        private readonly Dictionary<string, Term> terms = new();

        public NomenclatureBuilder AddTerm(Term term)
        {
            if (terms.ContainsKey(term.Name)) throw new ArgumentException("");
            terms.Add(term.Name, term);
            return this;
        }

        public NomenclatureBuilder AddTerms(IEnumerable<Term> terms)
        {
            foreach (Term term in terms)
            {
                AddTerm(term);
            }

            return this;
        }

        public Nomenclature CreateNomenclature(string name, string description)
        {
            Nomenclature nomenclature = new(name, description);
            AddTerms(nomenclature);
            return nomenclature;
        }

        public Nomenclature CreateNomenclature(string name)
        {
            Nomenclature nomenclature = new(name);
            AddTerms(nomenclature);
            return nomenclature;
        }

        private void AddTerms(Nomenclature nomenclature)
        {
            foreach (Term term in terms.Values)
            {
                nomenclature.Add(term);
            }

            terms.Clear();
        }
    }
}
