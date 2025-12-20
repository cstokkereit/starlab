using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of the chart grid used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Grid
    {
        [XmlAttribute("colour")]
        public string? Colour;

        [XmlElement]
        public GridLines? MajorGridLines;

        [XmlElement]
        public GridLines? MinorGridLines;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
