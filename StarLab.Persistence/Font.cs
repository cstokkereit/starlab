using System.Xml.Serialization;

namespace StarLab.Serialisation
{
    /// <summary>
    /// A POCO representation of a font used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Font
    {
        [XmlAttribute("familyName")]
        public string? FamilyName;

        [XmlAttribute("size")]
        public float Size;

        [XmlAttribute("fontStyle")]
        public string? FontStyle;
    }
}
