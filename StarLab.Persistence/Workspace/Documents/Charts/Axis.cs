using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of a chart axis used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Axis
    {
        [XmlAttribute("colour")]
        public string? Colour;

        [XmlElement]
        public Label? Label;

        [XmlElement]
        public Scale? Scale;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
