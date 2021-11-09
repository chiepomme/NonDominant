using System.Diagnostics;

namespace NonDominant
{
    public class VirtualKeyboard
    {
        readonly int[] pressCounts = new int[0xFF];

        public void Click(ActionKey key)
        {
            Debug.WriteLine($"[Click] {key}");

            WinAPI.DisableIME();

            if (key.Ctrl) SendKeyDown(VK.LCONTROL);
            if (key.Shift) SendKeyDown(VK.LSHIFT);
            if (key.Alt) SendKeyDown(VK.LMENU);
            if (key.Win) SendKeyDown(VK.LWIN);
            if (key.VK != VK.NONE) SendKeyDown(key.VK);
            if (key.VK != VK.NONE) SendKeyUp(key.VK);
            if (key.Win) SendKeyUp(VK.LWIN);
            if (key.Alt) SendKeyUp(VK.LMENU);
            if (key.Shift) SendKeyUp(VK.LSHIFT);
            if (key.Ctrl) SendKeyUp(VK.LCONTROL);
        }

        public void PressDown(ActionKey key)
        {
            Debug.WriteLine($"[Down] {key}");

            WinAPI.DisableIME();

            if (key.Ctrl) SendKeyDown(VK.LCONTROL);
            if (key.Shift) SendKeyDown(VK.LSHIFT);
            if (key.Alt) SendKeyDown(VK.LMENU);
            if (key.Win) SendKeyDown(VK.LWIN);
            if (key.VK != VK.NONE) SendKeyDown(key.VK);
        }

        public void PressUp(ActionKey key)
        {
            Debug.WriteLine($"[Up] {key}");

            WinAPI.DisableIME();

            if (key.VK != VK.NONE) SendKeyUp(key.VK);
            if (key.Win) SendKeyUp(VK.LWIN);
            if (key.Alt) SendKeyUp(VK.LMENU);
            if (key.Shift) SendKeyUp(VK.LSHIFT);
            if (key.Ctrl) SendKeyUp(VK.LCONTROL);
        }

        void SendKeyDown(VK key)
        {
            if (Interlocked.Increment(ref pressCounts[(byte)key]) == 1)
            {
                WinAPI.SendKeys(WinAPI.CreateKeyInput(key, true));
            }
        }

        void SendKeyUp(VK key)
        {
            if (Interlocked.Decrement(ref pressCounts[(byte)key]) == 0)
            {
                WinAPI.SendKeys(WinAPI.CreateKeyInput(key, false));
            }
        }
    }
}
