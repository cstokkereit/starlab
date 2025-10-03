using System.Xml.Serialization;

namespace Stratosoft.Nomenclature.Serialisation
{
    /// <summary>
    /// TODO
    /// </summary>
    static class NomenclatureSerialiser
    {
        public static Nomenclature Load(FileStream stream)
        {
            var serialiser = new XmlSerializer(typeof(XmlNomenclature));

            Nomenclature nomenclature;

            if (serialiser.Deserialize(stream) is XmlNomenclature state)
            {
                nomenclature = new Nomenclature(state);
            }
            else
            {
                throw new Exception(); // TODO
            }

            return nomenclature;
        }

        public static Nomenclature Load(string filename)
        {
            Nomenclature nomenclature;

            using (FileStream stream = File.OpenRead(filename))
            {
                nomenclature = Load(stream);
            }

            return nomenclature;
        }

        public static void Save(Nomenclature nomenclature, FileStream stream)
        {
            var serializer = new XmlSerializer(typeof(XmlNomenclature));

            serializer.Serialize(stream, BuildXmlNomenclature(nomenclature));
        }

        public static void SaveDictionary(Nomenclature nomenclature, string filename)
        {
            using (FileStream stream = File.OpenWrite(filename))
            {
                Save(nomenclature, stream);
            }
        }

        private static XmlNomenclature BuildXmlNomenclature(Nomenclature nomenclature)
        {
            var xmlNomenclature = new XmlNomenclature()
            {   
                Description = nomenclature.Description,
                Name = nomenclature.Name,
                ID = nomenclature.ID
            };

            return xmlNomenclature;
        }
    }
}
