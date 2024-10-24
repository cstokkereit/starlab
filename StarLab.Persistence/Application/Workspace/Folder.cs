using System.Xml.Serialization;

namespace StarLab.Application.Workspace
{
    [XmlType]
    public class Folder
    {
        [XmlAttribute("expanded")]
        public bool Expanded;

        [XmlAttribute("path")]
        public string? Path;
    }
}
