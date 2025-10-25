using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO representation of the tick marks used for XML serialisation/deserialisation.
    /// </summary>
    public class TickMarks
    {
        [XmlAttribute("visible")]
        public bool Visible;
    }
}
