using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RCT_Slave
{
    [XmlRoot("Config")]
    public class Config
    {

        [XmlElement("MasterIP")]
        public string MasterIP { get; set; }

        [XmlElement("MasterPort")]
        public int MasterPort { get; set; }

        [XmlElement("Token")]
        public string Token { get; set; }

        [XmlElement("WANMode")]
        public bool WanMode { get; set; }
    }
}
