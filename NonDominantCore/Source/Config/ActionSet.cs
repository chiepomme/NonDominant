using System.Xml.Serialization;

namespace NonDominant
{
    public class ActionSet
    {
        [XmlElement("down")]
        public ActionKey Down = new ActionKey();
        [XmlElement("l-down")]
        public ActionKey LDown = new ActionKey();
        [XmlElement("r-down")]
        public ActionKey RDown = new ActionKey();

        [XmlElement("hold")]
        public ActionKey Hold = new ActionKey();
        [XmlElement("l-hold")]
        public ActionKey LHold = new ActionKey();
        [XmlElement("r-hold")]
        public ActionKey RHold = new ActionKey();

        [XmlElement("click")]
        public ActionKey Click = new ActionKey();
        [XmlElement("l-click")]
        public ActionKey LClick = new ActionKey();
        [XmlElement("r-click")]
        public ActionKey RClick = new ActionKey();

        [XmlElement("double-click")]
        public ActionKey DoubleClick = new ActionKey();
        [XmlElement("l-double-click")]
        public ActionKey LDoubleClick = new ActionKey();
        [XmlElement("r-double-click")]
        public ActionKey RDoubleClick = new ActionKey();

        [XmlElement("up")]
        public ActionKey Up = new ActionKey();
        [XmlElement("l-up")]
        public ActionKey LUp = new ActionKey();
        [XmlElement("r-up")]
        public ActionKey RUp = new ActionKey();
    }
}
