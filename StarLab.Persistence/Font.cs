using System.Xml.Serialization;

namespace StarLab.Serialisation
{
    /// <summary>
    /// A POCO representation of a font used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Font
    {
        [XmlAttribute("bold")]
        public bool Bold;

        [XmlAttribute("family")]
        public string? Family;

        [XmlAttribute("italic")]
        public bool Italic;

        [XmlAttribute("size")]
        public float Size;

        [XmlAttribute("underline")]
        public bool Underline;
    }
}
