using System.Xml.Serialization;

namespace StarLab.Serialisation.Model
{
    [XmlType]
    public class Axis
    {
        [XmlAttribute("color")]
        public int Color;

        [XmlElement]
        public Font Font;

        [XmlAttribute("interval")]
        public double Interval;

        [XmlAttribute("isReversed")]
        public bool IsReversed;

        [XmlAttribute("maximum")]
        public double Maximum;

        [XmlAttribute("minimum")]
        public double Minimum;

        [XmlElement]
        public Title Title;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
