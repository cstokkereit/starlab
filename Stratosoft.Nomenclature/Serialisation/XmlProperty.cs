using System.Xml.Serialization;

namespace Stratosoft.Nomenclature.Serialisation
{
    /// <summary>
    /// A POCO representation of a property used for XML serialisation/deserialisation.
    /// </summary>
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
