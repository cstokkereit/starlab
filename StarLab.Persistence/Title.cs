using System.Xml.Serialization;

namespace StarLab.Serialisation
{
    /// <summary>
    /// A POCO representation of a title used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Title
    {
        //[XmlAttribute("alignment")]
        //public StringAlignment Alignment;

        [XmlAttribute("color")]
        public int Color;

        [XmlElement]
        public Font? Font;

        [XmlElement]
        public string? Text;
    }
}
