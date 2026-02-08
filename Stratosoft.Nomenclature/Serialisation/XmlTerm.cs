using System.Xml.Serialization;

namespace Stratosoft.Nomenclature.Serialisation
{
    /// <summary>
    /// A POCO representation of a term used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType("Term")]
    public class XmlTerm
    {
        public XmlTerm()
        {
            Properties = new List<XmlProperty>();
        }

        [XmlAttribute("description")]
        public string? Description;

        [XmlAttribute("id")]
        public Guid ID;

        [XmlAttribute("name")]
        public string? Name;

        [XmlArray]
        [XmlArrayItem(ElementName = "Property")]
        public List<XmlProperty> Properties;
    }
}
