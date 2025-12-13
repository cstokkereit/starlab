using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of the chart grid lines used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class GridLines
    {
        [XmlAttribute("backColour")]
        public string? BackColour;

        [XmlAttribute("foreColour")]
        public string? ForeColour;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
