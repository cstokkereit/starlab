using System.Xml.Serialization;

namespace Stratosoft.Nomenclature.Serialisation
{
    [XmlType("Property")]
    public class XmlProperty
    {
        [XmlAttribute("description")]
        public string? Description;

        [XmlAttribute("id")]
        public Guid ID;

        [XmlAttribute("name")]
        public string? Name;
    }
}
