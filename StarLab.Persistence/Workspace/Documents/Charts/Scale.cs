using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of an axis scale used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Scale
    {
        [XmlAttribute("autoscale")]
        public bool AutoScale;

        [XmlElement]
        public TickMarks? MajorTickMarks;

        [XmlAttribute("maximum")]
        public double Maximum;

        [XmlAttribute("minimum")]
        public double Minimum;

        [XmlElement]
        public TickMarks? MinorTickMarks;

        [XmlAttribute("reversed")]
        public bool Reversed;

        [XmlElement]
        public TickLabels? TickLabels;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
