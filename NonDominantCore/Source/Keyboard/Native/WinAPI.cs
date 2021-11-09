using System;
using System.Runtime.InteropServices;

namespace NonDominant
{
    public static class WinAPI
    {
        [DllImport("user32.dll")]
        static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);
        const uint INPUT_MOUSE = 0;
        const uint INPUT_KEYBOARD = 1;
        const uint INPUT_HARDWARE = 2;

        public static INPUT CreateKeyInput(VK key, bool isDown)
        {
            var input = new INPUT();
            input.type = INPUT_KEYBOARD;
            input.U.ki.wVk = key;
            input.U.ki.dwFlags = isDown ? 0 : KEYEVENTF.KEYUP;
            return input;
        }

        public static void SendKeys(params INPUT[] inputs)
        {
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<INPUT>());
        }

        [DllImport("Imm32.dll")]
        static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        const int WM_IME_CONTROL = 0x283;
        const int IMC_GETOPENSTATUS = 6;

        public static void DisableIME()
        {
            var imeWindowHandle = ImmGetDefaultIMEWnd(GetForegroundWindow());
            SendMessageW(imeWindowHandle, WM_IME_CONTROL, IMC_GETOPENSTATUS, IntPtr.Zero);
        }
    }
}
