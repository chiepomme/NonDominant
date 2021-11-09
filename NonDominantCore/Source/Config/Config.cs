using System.Xml.Serialization;

namespace NonDominant
{
    [XmlRoot("non-dominant-config")]
    public class Config
    {
        [XmlElement("default-app-config")]
        public AppConfig DefaultAppConfig { get; set; } = new AppConfig();

        [XmlArray("app-configs")]
        [XmlArrayItem("app-config")]
        public List<AppConfig> AppConfigs { get; set; } = new List<AppConfig>();
    }
}
