using Stratosoft.Nomenclature.Properties;

namespace Stratosoft.Nomenclature
{
    /// <summary>
    /// A class for constructing a <see cref="Nomenclature"/> that defines a system of names.
    /// </summary>
    public class NomenclatureBuilder
    {
        private readonly Dictionary<string, Term> terms = new(); // A dictionary containing the terms that will be included in the nomenclature.

        /// <summary>
        /// Adds a <see cref="Term"/> to the <see cref="Nomenclature"/>.
        /// </summary>
        /// <param name="term">The <see cref="Term"/> being added to the nomenclature.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        /// <exception cref="ArgumentException"></exception>
        public NomenclatureBuilder AddTerm(Term term)
        {
            ArgumentNullException.ThrowIfNull(term, nameof(term));

            if (terms.ContainsKey(term.Name)) throw new ArgumentException(string.Format(Resources.NamedTermAlreadyExists, term.Name), nameof(term));

            terms.Add(term.Name, term);

            return this;
        }

        /// <summary>
        /// Adds the terms in the <see cref="IEnumerable{Term}"/> provided to the <see cref="Nomenclature"/>.
        /// </summary>
        /// <param name="terms">An <see cref="IEnumerable{Term}"/> containing the terms being added to the nomenclature.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public NomenclatureBuilder AddTerms(IEnumerable<Term> terms)
        {
            ArgumentNullException.ThrowIfNull(terms, nameof(terms));

            foreach (Term term in terms)
            {
                AddTerm(term);
            }

            return this;
        }

        /// <summary>
        /// Creates the nomenclature.
        /// </summary>
        /// <param name="name">The name of the nomenclature.</param>
        /// <param name="description">A description of the nomenclature.</param>
        /// <returns>The specified <see cref="Nomenclature"/>.</returns>
        public Nomenclature CreateNomenclature(string name, string description)
        {
            Nomenclature nomenclature = new(name, description);
            AddTerms(nomenclature);
            return nomenclature;
        }

        /// <summary>
        /// Creates the nomenclature.
        /// </summary>
        /// <param name="name">The name of the nomenclature.</param>
        /// <returns>The specified <see cref="Nomenclature"/>.</returns>
        public Nomenclature CreateNomenclature(string name)
        {
            Nomenclature nomenclature = new(name);
            AddTerms(nomenclature);
            return nomenclature;
        }

        /// <summary>
        /// Adds the terms in the dictionary to the <see cref="Nomenclature"/> provided and clears the dictionary.
        /// </summary>
        /// <param name="nomenclature">The <see cref="Nomenclature"/> being constructed.</param>
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
