using StarLab.Application.Workspace.Documents;
using System.Xml.Serialization;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A POCO representation of a project used for XML serialisation/deserialisation.
    /// </summary>
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
