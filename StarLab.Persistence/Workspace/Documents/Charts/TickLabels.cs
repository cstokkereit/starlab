using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of the tick labels used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class TickLabels
    {
        [XmlAttribute("backColour")]
        public string? BackColour;

        [XmlElement]
        public Font? Font;

        [XmlAttribute("foreColour")]
        public string? ForeColour;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
