using Stratosoft.Nomenclature.Serialisation;
using System.Collections.ObjectModel;

namespace Stratosoft.Nomenclature
{
    /// <summary>
    /// 
    /// </summary>
    public class NomenclatureManager
    {
        private readonly Dictionary<string, INomenclature> nomenclatures = new(); // A dictionary containing the available nomenclatures indexed by name.

        /// <summary>
        /// Gets a <see cref="ReadOnlyDictionary{string, INomencalture}"/> containing the available nomenclatures.
        /// </summary>
        public ReadOnlyDictionary<string, INomenclature> Nomenclatures => new ReadOnlyDictionary<string, INomenclature>(nomenclatures);

        public void ExportDictionary(FileStream stream)
        {
            throw new NotImplementedException();
        }

        public void ExportDictionary(string filename)
        {
            throw new NotImplementedException();
        }

        public INomenclature FindDictionary(ITerm term)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<INomenclature> FindDictionaries(string termName)
        {
            throw new NotImplementedException();
        }

        public INomenclature FindTerm(IProperty property)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITerm> FindTerms(string propertyName)
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

        private string GetName(INomenclature nomenclature)
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
