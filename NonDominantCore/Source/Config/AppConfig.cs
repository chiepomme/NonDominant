using System.Xml;
using System.Xml.Serialization;

namespace NonDominant
{
    [XmlRoot("app-config")]
    public class AppConfig
    {
        [XmlElement("app-path")]
        public string AppPath { get; set; }

        [XmlElement("y")]
        public ActionSet Y { get; set; } = new ActionSet();
        [XmlElement("x")]
        public ActionSet X { get; set; } = new ActionSet();
        [XmlElement("b")]
        public ActionSet B { get; set; } = new ActionSet();
        [XmlElement("a")]
        public ActionSet A { get; set; } = new ActionSet();

        [XmlElement("right-sr")]
        public ActionSet RightSR { get; set; } = new ActionSet();
        [XmlElement("right-sl")]
        public ActionSet RightSL { get; set; } = new ActionSet();
        [XmlElement("r")]
        public ActionSet R { get; set; } = new ActionSet();
        [XmlElement("zr")]
        public ActionSet ZR { get; set; } = new ActionSet();

        [XmlElement("minus")]
        public ActionSet Minus { get; set; } = new ActionSet();
        [XmlElement("plus")]
        public ActionSet Plus { get; set; } = new ActionSet();
        [XmlElement("r-stick")]
        public ActionSet RStick { get; set; } = new ActionSet();
        [XmlElement("l-stick")]
        public ActionSet LStick { get; set; } = new ActionSet();
        [XmlElement("home")]
        public ActionSet Home { get; set; } = new ActionSet();
        [XmlElement("capture")]
        public ActionSet Capture { get; set; } = new ActionSet();
        [XmlElement("charging-grip")]
        public ActionSet ChargingGrip { get; set; } = new ActionSet();

        [XmlElement("down")]
        public ActionSet Down { get; set; } = new ActionSet();
        [XmlElement("up")]
        public ActionSet Up { get; set; } = new ActionSet();
        [XmlElement("right")]
        public ActionSet Right { get; set; } = new ActionSet();
        [XmlElement("left")]
        public ActionSet Left { get; set; } = new ActionSet();

        [XmlElement("left-sr")]
        public ActionSet LeftSR { get; set; } = new ActionSet();
        [XmlElement("left-sl")]
        public ActionSet LeftSL { get; set; } = new ActionSet();
        [XmlElement("l")]
        public ActionSet L { get; set; } = new ActionSet();
        [XmlElement("zl")]
        public ActionSet ZL { get; set; } = new ActionSet();

        [XmlElement("l-stick-up")]
        public ActionSet LStickUp { get; set; } = new ActionSet();
        [XmlElement("l-stick-up-left")]
        public ActionSet LStickUpLeft { get; set; } = new ActionSet();
        [XmlElement("l-stick-up-right")]
        public ActionSet LStickUpRight { get; set; } = new ActionSet();
        [XmlElement("l-stick-down")]
        public ActionSet LStickDown { get; set; } = new ActionSet();
        [XmlElement("l-stick-down-left")]
        public ActionSet LStickDownLeft { get; set; } = new ActionSet();
        [XmlElement("l-stick-down-right")]
        public ActionSet LStickDownRight { get; set; } = new ActionSet();

        [XmlElement("r-stick-up")]
        public ActionSet RStickUp { get; set; } = new ActionSet();
        [XmlElement("r-stick-up-left")]
        public ActionSet RStickUpLeft { get; set; } = new ActionSet();
        [XmlElement("r-stick-up-right")]
        public ActionSet RStickUpRight { get; set; } = new ActionSet();
        [XmlElement("r-stick-down")]
        public ActionSet RStickDown { get; set; } = new ActionSet();
        [XmlElement("r-stick-down-left")]
        public ActionSet RStickDownLeft { get; set; } = new ActionSet();
        [XmlElement("r-stick-down-right")]
        public ActionSet RStickDownRight { get; set; } = new ActionSet();
    }
}
