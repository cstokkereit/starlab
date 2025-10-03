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

        //[XmlAttribute("interval")]
        //public double Interval;

        //[XmlAttribute("isReversed")]
        //public bool IsReversed;

        //[XmlAttribute("maximum")]
        //public double Maximum;

        //[XmlAttribute("minimum")]
        //public double Minimum;

        [XmlElement]
        public Label? Label;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
