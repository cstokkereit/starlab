using Stratosoft.Nomenclature.Serialisation;
using System.Collections.ObjectModel;

namespace Stratosoft.Nomenclature
{
    public class NomenclatureManager
    {
        private readonly Dictionary<string, Nomenclature> nomenclatures = new();

        public ReadOnlyDictionary<string, Nomenclature> Nomenclatures => new ReadOnlyDictionary<string, Nomenclature>(nomenclatures);

        public void ExportDictionary(FileStream stream)
        {
            throw new NotImplementedException();
        }

        public void ExportDictionary(string filename)
        {
            throw new NotImplementedException();
        }

        public Nomenclature FindDictionary(Term term)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Nomenclature> FindDictionaries(string termName)
        {
            throw new NotImplementedException();
        }

        public Nomenclature FindTerm(Property property)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Term> FindTerms(string propertyName)
        {
            throw new NotImplementedException();
        }

        public void ImportDictionary(FileStream stream)
        {
            var dictionary = NomenclatureSerialiser.Load(stream);

            var name = GetName(dictionary);

            if (nomenclatures.ContainsKey(name))
            {
                // This is a new version of an existing dictionary
                nomenclatures[name] = dictionary;
            }
            else
            {
                // This is a new dictionary
                nomenclatures.Add(name, dictionary);
            }
        }

        public void ImportDictionary(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                ImportDictionary(stream);
            }
        }

        private string GetName(Nomenclature nomenclature)
        {
            var name = nomenclature.Name;
            
            if (nomenclatures.ContainsKey(name) && nomenclature.ID != nomenclatures[name].ID)
            {
                var n = 2;

                while (nomenclatures.ContainsKey(name))
                {
                    name = nomenclature.Name + "(" + n++ + ")";
                }
            }
            
            return name;
        }
    }
}
