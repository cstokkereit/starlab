using System.Xml.Serialization;

namespace StarLab.Serialisation.Model
{
    [XmlType]
    public class Grid
    {
        [XmlAttribute("color")]
        public int Color;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
