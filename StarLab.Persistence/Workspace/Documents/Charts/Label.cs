using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of a label used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Label
    {
        [XmlAttribute("backColour")]
        public string? BackColour;

        [XmlElement]
        public Font? Font;

        [XmlAttribute("foreColour")]
        public string? ForeColour;

        [XmlAttribute("text")]
        public string? Text;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
