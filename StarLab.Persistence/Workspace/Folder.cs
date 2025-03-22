using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace
{
    /// <summary>
    /// A POCO representation of a folder used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Folder
    {
        [XmlAttribute("expanded")]
        public bool Expanded;

        [XmlAttribute("path")]
        public string? Path;
    }
}
