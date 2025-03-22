using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of a chart used for XML serialisation/deserialisation.
    /// </summary>
    [XmlType]
    public class Chart
    {
        [XmlAttribute("id")]
        public int ID;

        [XmlAttribute("name")]
        public string? Name;

        [XmlAttribute("path")]
        public string? Path;

        [XmlAttribute("type")]
        public string? Type;

        [XmlAttribute("backColor")]
        public int BackColor;

        [XmlAttribute("foreColor")]
        public int ForeColor;

        [XmlElement]
        public Grid? MajorGrid;

        [XmlElement]
        public Grid? MinorGrid;

        [XmlElement]
        public Axis? XAxis;

        [XmlElement]
        public Axis? XAxis2;

        [XmlElement]
        public Axis? YAxis;

        [XmlElement]
        public Axis? YAxis2;
    }
}
