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


        // Events


        [XmlElement("Event1Name")]
        public string Event1Name { get; set; }

        [XmlElement("Event1Path")]
        public string Event1Path { get; set; }

        [XmlElement("Event2Name")]
        public string Event2Name { get; set; }

        [XmlElement("Event2Path")]
        public string Event2Path { get; set; }

        [XmlElement("Event3Name")]
        public string Event3Name { get; set; }

        [XmlElement("Event3Path")]
        public string Event3Path { get; set; }

        [XmlElement("Event4Name")]
        public string Event4Name { get; set; }

        [XmlElement("Event4Path")]
        public string Event4Path { get; set; }

        [XmlElement("Event5Name")]
        public string Event5Name { get; set; }

        [XmlElement("Event5Path")]
        public string Event5Path { get; set; }

        [XmlElement("Event6Name")]
        public string Event6Name { get; set; }

        [XmlElement("Event6Path")]
        public string Event6Path { get; set; }

        [XmlElement("Event7Name")]
        public string Event7Name { get; set; }

        [XmlElement("Event7Path")]
        public string Event7Path { get; set; }

        [XmlElement("Event8Name")]
        public string Event8Name { get; set; }

        [XmlElement("Event8Path")]
        public string Event8Path { get; set; }

        [XmlElement("Event9Name")]
        public string Event9Name { get; set; }

        [XmlElement("Event9Path")]
        public string Event9Path { get; set; }

        [XmlElement("Event10Name")]
        public string Event10Name { get; set; }

        [XmlElement("Event10Path")]
        public string Event10Path { get; set; }

        [XmlElement("Event11Name")]
        public string Event11Name { get; set; }

        [XmlElement("Event11Path")]
        public string Event11Path { get; set; }

        [XmlElement("Event12Name")]
        public string Event12Name { get; set; }

        [XmlElement("Event12Path")]
        public string Event12Path { get; set; }

        [XmlElement("Event13Name")]
        public string Event13Name { get; set; }

        [XmlElement("Event13Path")]
        public string Event13Path { get; set; }

        [XmlElement("Event14Name")]
        public string Event14Name { get; set; }

        [XmlElement("Event14Path")]
        public string Event14Path { get; set; }
    }
}
