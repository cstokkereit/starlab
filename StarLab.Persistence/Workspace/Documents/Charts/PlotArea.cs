using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of the chart plot area used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class PlotArea
    {
        [XmlAttribute("backColour")]
        public string? BackColour;

        [XmlAttribute("foreColour")]
        public string? ForeColour;

        [XmlElement]
        public Grid? Grid;
    }
}
