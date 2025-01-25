using System.Xml.Serialization;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A POCO representation of a document used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Document
    {
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
