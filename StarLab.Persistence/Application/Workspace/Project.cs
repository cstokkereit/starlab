using StarLab.Application.Workspace.Documents;
using System.Xml.Serialization;

namespace StarLab.Application.Workspace
{
    public class Project
    {
        [XmlArray]
        public List<Document>? Documents;

        [XmlAttribute("expanded")]
        public bool Expanded;

        [XmlArray]
        public List<Folder>? Folders;

        [XmlAttribute("name")]
        public string? Name;
    }
}
