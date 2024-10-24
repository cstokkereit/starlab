using System.Xml.Serialization;

namespace StarLab.Application.Workspace.Documents.Charts
{
    [XmlType]
    public class Grid
    {
        [XmlAttribute("color")]
        public int Color;

        [XmlAttribute("visible")]
        public bool Visible;
    }
}
