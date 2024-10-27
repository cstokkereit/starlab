using System.Xml.Serialization;

namespace StarLab.Application
{
    [XmlType]
    public class Title
    {
        //[XmlAttribute("alignment")]
        //public StringAlignment Alignment;

        [XmlAttribute("color")]
        public int Color;

        [XmlElement]
        public Font Font;

        [XmlElement]
        public string Text;
    }
}
