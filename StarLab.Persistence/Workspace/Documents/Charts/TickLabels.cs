using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of the tick labels used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class TickLabels
    {
        [XmlAttribute("colour")]
        public string? Colour;

        [XmlElement]
        public Font? Font;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
