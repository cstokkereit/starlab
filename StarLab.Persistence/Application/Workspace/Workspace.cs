using StarLab.Application.Workspace.Documents;
using System.Xml;
using System.Xml.Serialization;

namespace StarLab.Application.Workspace
{
    [XmlRoot]
    public class Workspace
    {
        [XmlArray]
        public List<Document>? Documents;

        [XmlArray]
        public List<Folder>? Folders;

        [XmlIgnore]
        public string? Layout;

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
