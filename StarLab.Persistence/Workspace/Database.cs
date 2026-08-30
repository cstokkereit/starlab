using System.Xml.Serialization;

namespace StarLab.Serialisation.Workspace
{
    /// <summary>
    /// A POCO representation of a database used for XML serialisation/deserialisation.
    /// </summary>
    public class Database
    {
        [XmlAttribute("host")]
        public string? Host;

        [XmlAttribute("port")]
        public int Port;

        [XmlAttribute("name")]
        public string? Name;
    }
}
