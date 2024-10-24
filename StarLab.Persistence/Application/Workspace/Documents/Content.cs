using System.Xml.Serialization;

namespace StarLab.Application.Workspace.Documents
{
    [XmlType]
    public class Content
    {
        [XmlAttribute("name")]
        public string? Name;

        [XmlAttribute("panel")]
        public int Panel;

        [XmlAttribute("view")]
        public string? View;
    }
}
