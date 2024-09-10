using System.Xml.Serialization;

namespace StarLab.Serialisation.Model
{
    [XmlType]
    public class Document
    {
        [XmlElement]
        public Content Content;

        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("path")]
        public string Path;

        [XmlAttribute("type")]
        public string Type;

        [XmlAttribute("view")]
        public string View;
    }
}
