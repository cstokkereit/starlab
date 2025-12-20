using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of the tick marks used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class TickMarks
    {
        [XmlAttribute("colour")]
        public string? Colour;

        [XmlAttribute("length")]
        public int Length;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
