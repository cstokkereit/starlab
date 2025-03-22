using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of a chart grid used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Grid
    {
        [XmlAttribute("color")]
        public int Color;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
