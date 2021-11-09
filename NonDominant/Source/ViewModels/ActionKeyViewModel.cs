using NonDominant;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace NonDominant.ViewModels
{
    public class KeyComboItem
    {
        public VK Key { get; set; }
        public string Display { get; set; }
    }

    class ActionKeyViewModel : BindableBase
    {
        public ActionKeyViewModel(ActionKey key)
        {
            Key = KeyItems.FirstOrDefault(ki => ki.Key == key.VK) ?? KeyItems[0];
            Ctrl = key.Ctrl;
            Shift = key.Shift;
            Win = key.Win;
        }

        public KeyComboItem Key { get; set; }
        public bool Ctrl { get; set; }
        public bool Alt { get; set; }
        public bool Shift { get; set; }
        public bool Win { get; set; }

        public ActionKey ToModel() => new ActionKey(Key.Key, Ctrl, Alt, Shift, Win);

        public List<KeyComboItem> KeyItems { get; set; } = new List<KeyComboItem>
        {
            new KeyComboItem{ Key = VK.NONE, Display = "なし" },
            new KeyComboItem{ Key = VK.KEY_A, Display = "A" },
            new KeyComboItem{ Key = VK.KEY_B, Display = "B" },
            new KeyComboItem{ Key = VK.KEY_C, Display = "C" },
            new KeyComboItem{ Key = VK.KEY_D, Display = "D" },
            new KeyComboItem{ Key = VK.KEY_E, Display = "E" },
            new KeyComboItem{ Key = VK.KEY_F, Display = "F" },
            new KeyComboItem{ Key = VK.KEY_G, Display = "G" },
            new KeyComboItem{ Key = VK.KEY_H, Display = "H" },
            new KeyComboItem{ Key = VK.KEY_I, Display = "I" },
            new KeyComboItem{ Key = VK.KEY_J, Display = "J" },
            new KeyComboItem{ Key = VK.KEY_K, Display = "K" },
            new KeyComboItem{ Key = VK.KEY_L, Display = "L" },
            new KeyComboItem{ Key = VK.KEY_M, Display = "M" },
            new KeyComboItem{ Key = VK.KEY_N, Display = "N" },
            new KeyComboItem{ Key = VK.KEY_O, Display = "O" },
            new KeyComboItem{ Key = VK.KEY_P, Display = "P" },
            new KeyComboItem{ Key = VK.KEY_Q, Display = "Q" },
            new KeyComboItem{ Key = VK.KEY_R, Display = "R" },
            new KeyComboItem{ Key = VK.KEY_S, Display = "S" },
            new KeyComboItem{ Key = VK.KEY_T, Display = "T" },
            new KeyComboItem{ Key = VK.KEY_U, Display = "U" },
            new KeyComboItem{ Key = VK.KEY_V, Display = "V" },
            new KeyComboItem{ Key = VK.KEY_W, Display = "W" },
            new KeyComboItem{ Key = VK.KEY_X, Display = "X" },
            new KeyComboItem{ Key = VK.KEY_Y, Display = "Y" },
            new KeyComboItem{ Key = VK.KEY_Z, Display = "Z" },

            new KeyComboItem{ Key = VK.KEY_0, Display = "0" },
            new KeyComboItem{ Key = VK.KEY_1, Display = "1" },
            new KeyComboItem{ Key = VK.KEY_2, Display = "2" },
            new KeyComboItem{ Key = VK.KEY_3, Display = "3" },
            new KeyComboItem{ Key = VK.KEY_4, Display = "4" },
            new KeyComboItem{ Key = VK.KEY_5, Display = "5" },
            new KeyComboItem{ Key = VK.KEY_6, Display = "6" },
            new KeyComboItem{ Key = VK.KEY_7, Display = "7" },
            new KeyComboItem{ Key = VK.KEY_8, Display = "8" },
            new KeyComboItem{ Key = VK.KEY_9, Display = "9" },

            new KeyComboItem{ Key = VK.F1, Display = "F1" },
            new KeyComboItem{ Key = VK.F2, Display = "F2" },
            new KeyComboItem{ Key = VK.F3, Display = "F3" },
            new KeyComboItem{ Key = VK.F4, Display = "F4" },
            new KeyComboItem{ Key = VK.F5, Display = "F5" },
            new KeyComboItem{ Key = VK.F6, Display = "F6" },
            new KeyComboItem{ Key = VK.F7, Display = "F7" },
            new KeyComboItem{ Key = VK.F8, Display = "F8" },
            new KeyComboItem{ Key = VK.F9, Display = "F9" },
            new KeyComboItem{ Key = VK.F10, Display = "F10" },
            new KeyComboItem{ Key = VK.F11, Display = "F11" },
            new KeyComboItem{ Key = VK.F12, Display = "F12" },
            new KeyComboItem{ Key = VK.F13, Display = "F13" },
            new KeyComboItem{ Key = VK.F14, Display = "F14" },
            new KeyComboItem{ Key = VK.F15, Display = "F15" },
            new KeyComboItem{ Key = VK.F16, Display = "F16" },
            new KeyComboItem{ Key = VK.F17, Display = "F17" },
            new KeyComboItem{ Key = VK.F18, Display = "F18" },
            new KeyComboItem{ Key = VK.F19, Display = "F19" },
            new KeyComboItem{ Key = VK.F20, Display = "F20" },
            new KeyComboItem{ Key = VK.F21, Display = "F21" },
            new KeyComboItem{ Key = VK.F22, Display = "F22" },
            new KeyComboItem{ Key = VK.F23, Display = "F23" },
            new KeyComboItem{ Key = VK.F24, Display = "F24" },

            new KeyComboItem{ Key = VK.SPACE, Display = "Space" },
            new KeyComboItem{ Key = VK.RETURN, Display = "Enter" },
            new KeyComboItem{ Key = VK.ESCAPE, Display = "Esc" },
            new KeyComboItem{ Key = VK.OEM_COMMA, Display = "," },
            new KeyComboItem{ Key = VK.OEM_PERIOD, Display = "." },
            new KeyComboItem{ Key = VK.OEM_PLUS, Display = ";" },
            new KeyComboItem{ Key = VK.OEM_1, Display = ":" },
            new KeyComboItem{ Key = VK.OEM_2, Display = "/" },
            new KeyComboItem{ Key = VK.OEM_102, Display = "\\" },
            new KeyComboItem{ Key = VK.OEM_3, Display = "@" },
            new KeyComboItem{ Key = VK.OEM_4, Display = "[" },
            new KeyComboItem{ Key = VK.OEM_6, Display = "]" },
            new KeyComboItem{ Key = VK.OEM_7, Display = "^" },
            new KeyComboItem{ Key = VK.OEM_MINUS, Display = "-" },

            new KeyComboItem{ Key = VK.PRIOR, Display = "PageUp" },
            new KeyComboItem{ Key = VK.NEXT, Display = "PageDown" },
            new KeyComboItem{ Key = VK.HOME, Display = "Home" },
            new KeyComboItem{ Key = VK.END, Display = "End" },
            new KeyComboItem{ Key = VK.INSERT, Display = "Insert" },

            new KeyComboItem{ Key = VK.SHIFT, Display = "Shift" },
            new KeyComboItem{ Key = VK.LSHIFT, Display = "Shift (左)" },
            new KeyComboItem{ Key = VK.RSHIFT, Display = "Shift (右)" },

            new KeyComboItem{ Key = VK.CONTROL, Display = "Ctrl" },
            new KeyComboItem{ Key = VK.LCONTROL, Display = "Ctrl (左)" },
            new KeyComboItem{ Key = VK.RCONTROL, Display = "Ctrl (右)" },

            new KeyComboItem{ Key = VK.MENU, Display = "Alt" },
            new KeyComboItem{ Key = VK.LMENU, Display = "Alt (左)" },
            new KeyComboItem{ Key = VK.RMENU, Display = "Alt (右)" },

            new KeyComboItem{ Key = VK.LWIN, Display = "Windows (左)" },
            new KeyComboItem{ Key = VK.RWIN, Display = "Windows (右)" },
            new KeyComboItem{ Key = VK.APPS, Display = "Context (Apps)" },

            new KeyComboItem{ Key = VK.BACK, Display = "Back Space" },
            new KeyComboItem{ Key = VK.DELETE, Display = "Delete" },

            new KeyComboItem{ Key = VK.TAB, Display = "Tab" },
            new KeyComboItem{ Key = VK.CAPITAL, Display = "CapsLock" },
            new KeyComboItem{ Key = VK.NUMLOCK, Display = "NumLock" },

            new KeyComboItem{ Key = VK.PRINT, Display = "PrintScreen" },
            new KeyComboItem{ Key = VK.SCROLL, Display = "ScrollLock" },
            new KeyComboItem{ Key = VK.PAUSE, Display = "Pause" },

            new KeyComboItem{ Key = VK.NUMPAD0, Display = "0 (テンキー)" },
            new KeyComboItem{ Key = VK.NUMPAD1, Display = "1 (テンキー)" },
            new KeyComboItem{ Key = VK.NUMPAD2, Display = "2 (テンキー)" },
            new KeyComboItem{ Key = VK.NUMPAD3, Display = "3 (テンキー)" },
            new KeyComboItem{ Key = VK.NUMPAD4, Display = "4 (テンキー)" },
            new KeyComboItem{ Key = VK.NUMPAD5, Display = "5 (テンキー)" },
            new KeyComboItem{ Key = VK.NUMPAD6, Display = "6 (テンキー)" },
            new KeyComboItem{ Key = VK.NUMPAD7, Display = "7 (テンキー)" },
            new KeyComboItem{ Key = VK.NUMPAD8, Display = "8 (テンキー)" },
            new KeyComboItem{ Key = VK.NUMPAD9, Display = "9 (テンキー)" },

            new KeyComboItem{ Key = VK.ADD, Display = "+ (テンキー)" },
            new KeyComboItem{ Key = VK.SUBTRACT, Display = "- (テンキー)" },
            new KeyComboItem{ Key = VK.MULTIPLY, Display = "* (テンキー)" },
            new KeyComboItem{ Key = VK.DIVIDE, Display = "/ (テンキー)" },
            new KeyComboItem{ Key = VK.DECIMAL, Display = ". (テンキー)" },

            new KeyComboItem{ Key = VK.BROWSER_BACK, Display = "ブラウザ戻る" },
            new KeyComboItem{ Key = VK.BROWSER_FORWARD, Display = "ブラウザ進む" },
        };
    }
}
