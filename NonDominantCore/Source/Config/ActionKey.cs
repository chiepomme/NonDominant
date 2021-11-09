using System.Text;
using System.Xml.Serialization;

namespace NonDominant
{
    public class ActionKey
    {
        [XmlAttribute("key")]
        public VK VK;
        [XmlAttribute("ctrl")]
        public bool Ctrl;
        [XmlAttribute("alt")]
        public bool Alt;
        [XmlAttribute("shift")]
        public bool Shift;
        [XmlAttribute("win")]
        public bool Win;

        public bool Empty => VK == VK.NONE && !Ctrl && !Alt && !Shift && !Win;

        public ActionKey() { }

        public ActionKey(VK vk, bool ctrl = false, bool alt = false, bool shift = false, bool win = false)
        {
            VK = vk;
            Ctrl = ctrl;
            Alt = alt;
            Shift = shift;
            Win = win;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Ctrl) sb.Append("Ctrl + ");
            if (Shift) sb.Append("Shift + ");
            if (Alt) sb.Append("Alt + ");
            if (Win) sb.Append("Win + ");

            sb.Append(VK);

            return sb.ToString();
        }
    }
}
