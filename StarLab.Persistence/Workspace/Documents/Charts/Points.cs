using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of the chart data points used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Points
    {
        [XmlAttribute("colour")]
        public string? Colour;

        [XmlAttribute("size")]
        public int Size;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
