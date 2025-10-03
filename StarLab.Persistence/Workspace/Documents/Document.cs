using StarLab.Serialisation.Workspace.Documents.Charts;
using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents
{
    /// <summary>
    /// A POCO representation of a document used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Document
    {
        [XmlElement]
        public Chart? Chart;

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
