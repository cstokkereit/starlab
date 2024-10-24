using System.Xml.Serialization;

namespace StarLab.Application
{
    [XmlType]
    public class Font
    {
        [XmlAttribute("familyName")]
        public string FamilyName;

        [XmlAttribute("size")]
        public float Size;

        [XmlAttribute("fontStyle")]
        public string FontStyle;
    }
}
