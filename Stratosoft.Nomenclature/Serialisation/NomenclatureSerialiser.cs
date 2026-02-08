using System.Xml.Serialization;

namespace Stratosoft.Nomenclature.Serialisation
{
    /// <summary>
    /// Serialises and deserialises the <see cref="XmlNomenclature"> class.
    /// </summary>
    static class NomenclatureSerialiser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Nomenclature Load(string filename)
        {
            Nomenclature nomenclature;

            using (FileStream stream = File.OpenRead(filename))
            {
                nomenclature = Load(stream);
            }

            return nomenclature;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomenclature"></param>
        /// <param name="stream"></param>
        public static void Save(Nomenclature nomenclature, FileStream stream)
        {
            var serializer = new XmlSerializer(typeof(XmlNomenclature));

            serializer.Serialize(stream, BuildXmlNomenclature(nomenclature));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomenclature"></param>
        /// <param name="filename"></param>
        public static void SaveDictionary(Nomenclature nomenclature, string filename)
        {
            using (FileStream stream = File.OpenWrite(filename))
            {
                Save(nomenclature, stream);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomenclature"></param>
        /// <returns></returns>
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
