using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of a chart used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Chart
    {
        [XmlAttribute("backColour")]
        public string? BackColour;

        [XmlElement]
        public Font? Font;

        [XmlAttribute("foreColour")]
        public string? ForeColour;

        [XmlElement]
        public PlotArea? PlotArea;

        [XmlElement]
        public Label? Title;

        [XmlElement]
        public Axis? X1;

        [XmlElement]
        public Axis? X2;

        [XmlElement]
        public Axis? Y1;

        [XmlElement]
        public Axis? Y2;
    }
}
