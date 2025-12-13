using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of the chart grid used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Grid
    {
        [XmlAttribute("backColour")]
        public string? BackColour;

        [XmlAttribute("foreColour")]
        public string? ForeColour;

        [XmlElement]
        public GridLines? MajorGridLines;

        [XmlElement]
        public GridLines? MinorGridLines;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
