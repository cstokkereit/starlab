using System.Xml;
using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace
{
    /// <summary>
    /// A POCO representation of a workspace used for XML serialisation/deserialisation.
    /// </summary>
    [XmlRoot]
    public class Workspace
    {
        [XmlAttribute]
        public string? ActiveDocument;

        [XmlIgnore]
        public string? Layout;

        [XmlArray]
        public List<Project>? Projects;

        [XmlElement("Layout")]
        public XmlCDataSection LayoutCData
        {
            get
            {
                var document = new XmlDocument();
                return document.CreateCDataSection(Layout);
            }

            set
            {
                Layout = value == null ? string.Empty : value.Value;
            }
        }
    }
}
