using System.Xml;
using System.Xml.Serialization;

namespace StarLab.Serialisation.Model
{
    [XmlRoot]
    public class Workspace
    {
        [XmlArray]
        public List<Chart>? Charts;

        [XmlAttribute("filename")]
        public string? FileName;

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
