using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of a chart axis used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Axis
    {
        [XmlAttribute("backColour")]
        public string? BackColour;

        [XmlAttribute("foreColour")]
        public string? ForeColour;

        [XmlElement]
        public Label? Label;

        [XmlElement]
        public Scale? Scale;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
