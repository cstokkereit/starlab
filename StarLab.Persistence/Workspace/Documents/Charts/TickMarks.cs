using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of the tick marks used for XML serialisation/deserialisation.
    /// </summary>
    public class TickMarks
    {
        [XmlAttribute("backColour")]
        public string? BackColour;

        [XmlAttribute("foreColour")]
        public string? ForeColour;

        [XmlAttribute("length")]
        public int Length;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
