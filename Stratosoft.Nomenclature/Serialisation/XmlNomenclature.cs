using System.Xml.Serialization;

namespace Stratosoft.Nomenclature.Serialisation
{
    [XmlType("Nomenclature")]
    public class XmlNomenclature
    {
        public XmlNomenclature()
        {
            Terms = new List<XmlTerm>();
        }

        [XmlAttribute("description")]
        public string? Description;

        [XmlAttribute("id")]
        public Guid ID;

        [XmlAttribute("name")]
        public string? Name;

        [XmlArray]
        [XmlArrayItem(ElementName="Term")]
        public List<XmlTerm> Terms;
    }
}
