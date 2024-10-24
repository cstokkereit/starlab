using System.Xml.Serialization;

namespace StarLab.Application.Workspace.Documents
{
    [XmlType]
    public class Document
    {
        [XmlArray]
        public List<Content>? Contents;

        [XmlAttribute("id")]
        public string? ID;

        [XmlAttribute("name")]
        public string? Name;

        [XmlAttribute("path")]
        public string? Path;

        [XmlAttribute("view")]
        public string? View;
    }
}
