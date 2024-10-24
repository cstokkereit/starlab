using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StarLab.Application
{
    [XmlType]
    public class Title
    {
        //[XmlAttribute("alignment")]
        //public StringAlignment Alignment;

        [XmlAttribute("color")]
        public int Color;

        [XmlElement]
        public Font Font;

        [XmlElement]
        public string Text;
    }
}
