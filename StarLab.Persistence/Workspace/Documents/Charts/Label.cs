using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of a label used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Label
    {
        [XmlAttribute("colour")]
        public string? Colour;

        [XmlElement]
        public Font? Font;

        [XmlAttribute("text")]
        public string? Text;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
