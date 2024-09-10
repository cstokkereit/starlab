using System.Xml.Serialization;

namespace StarLab.Serialisation.Model
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
