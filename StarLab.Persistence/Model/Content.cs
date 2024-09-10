using System.Xml.Serialization;

namespace StarLab.Serialisation.Model
{
    [XmlType]
    public class Content
    {
        [XmlElement("Content")]
        public List<Content>? Contents;

        [XmlAttribute("name")]
        public string? Name;

        [XmlAttribute("panel")]
        public int Panel;

        [XmlAttribute("view")]
        public string? View;
    }
}
