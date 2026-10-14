using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A POCO representation of a chart overlay used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Overlay
    {
        [XmlAttribute("colour")]
        public string Colour = string.Empty;

        [XmlElement]
        public Font? Font;

        [XmlAttribute("name")]
        public string? Name;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
