using System.Text;

namespace JoyConSharp
{
    /// <summary>
    /// https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/bluetooth_hid_notes.md#standard-input-report-format
    /// </summary>
    public class StandardInputReport
    {
        public readonly byte Timer;

        public readonly bool Y;
        public readonly bool X;
        public readonly bool B;
        public readonly bool A;
        public readonly bool RightSR;
        public readonly bool RightSL;
        public readonly bool R;
        public readonly bool ZR;

        public readonly bool Minus;
        public readonly bool Plus;
        public readonly bool RStick;
        public readonly bool LStick;
        public readonly bool Home;
        public readonly bool Capture;
        public readonly bool ChargingGrip;

        public readonly bool Down;
        public readonly bool Up;
        public readonly bool Right;
        public readonly bool Left;
        public readonly bool LeftSR;
        public readonly bool LeftSL;
        public readonly bool L;
        public readonly bool ZL;

        public readonly int LeftStickX;
        public readonly int LeftStickY;
        public readonly int RightStickX;
        public readonly int RightStickY;

        public readonly JoyConButtons PressedButtons;

        public bool GetPressed(JoyConButtons button) => PressedButtons.HasFlag(button);

        public StandardInputReport(in ReadOnlySpan<byte> report)
        {
            Timer = report[1];

            DeserializeButton(ref PressedButtons, report[3], 0x01, JoyConButtons.Y, ref Y);
            DeserializeButton(ref PressedButtons, report[3], 0x02, JoyConButtons.X, ref X);
            DeserializeButton(ref PressedButtons, report[3], 0x04, JoyConButtons.B, ref B);
            DeserializeButton(ref PressedButtons, report[3], 0x08, JoyConButtons.A, ref A);

            DeserializeButton(ref PressedButtons, report[3], 0x10, JoyConButtons.RightSR, ref RightSR);
            DeserializeButton(ref PressedButtons, report[3], 0x20, JoyConButtons.RightSL, ref RightSL);
            DeserializeButton(ref PressedButtons, report[3], 0x40, JoyConButtons.R, ref R);
            DeserializeButton(ref PressedButtons, report[3], 0x80, JoyConButtons.ZR, ref ZR);


            DeserializeButton(ref PressedButtons, report[4], 0x01, JoyConButtons.Minus, ref Minus);
            DeserializeButton(ref PressedButtons, report[4], 0x02, JoyConButtons.Plus, ref Plus);
            DeserializeButton(ref PressedButtons, report[4], 0x04, JoyConButtons.RStick, ref RStick);
            DeserializeButton(ref PressedButtons, report[4], 0x08, JoyConButtons.LStick, ref LStick);

            DeserializeButton(ref PressedButtons, report[4], 0x10, JoyConButtons.Home, ref Home);
            DeserializeButton(ref PressedButtons, report[4], 0x20, JoyConButtons.Capture, ref Capture);
            DeserializeButton(ref PressedButtons, report[4], 0x80, JoyConButtons.ChargingGrip, ref ChargingGrip);


            DeserializeButton(ref PressedButtons, report[5], 0x01, JoyConButtons.Down, ref Down);
            DeserializeButton(ref PressedButtons, report[5], 0x02, JoyConButtons.Up, ref Up);
            DeserializeButton(ref PressedButtons, report[5], 0x04, JoyConButtons.Right, ref Right);
            DeserializeButton(ref PressedButtons, report[5], 0x08, JoyConButtons.Left, ref Left);

            DeserializeButton(ref PressedButtons, report[5], 0x10, JoyConButtons.LeftSR, ref LeftSR);
            DeserializeButton(ref PressedButtons, report[5], 0x20, JoyConButtons.LeftSL, ref LeftSL);
            DeserializeButton(ref PressedButtons, report[5], 0x40, JoyConButtons.L, ref L);
            DeserializeButton(ref PressedButtons, report[5], 0x80, JoyConButtons.ZL, ref ZL);

            LeftStickX = report[6] | ((report[7] & 0xF) << 8);
            LeftStickY = (report[7] >> 4) | (report[8] << 4);
            RightStickX = report[9] | ((report[10] & 0xF) << 8);
            RightStickY = (report[10] >> 4) | (report[11] << 4);
        }

        static void DeserializeButton(ref JoyConButtons pressedButtons, byte reportByte, byte buttonBits, JoyConButtons button, ref bool pressed)
        {
            if ((reportByte & buttonBits) == buttonBits)
            {
                pressed = true;
                pressedButtons |= button;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"LX:{LeftStickX} ");
            sb.Append($"LY:{LeftStickY} ");
            sb.Append($"RX:{RightStickX} ");
            sb.Append($"RY:{RightStickY} ");

            if (Y) sb.Append("Y ");
            if (X) sb.Append("X ");
            if (B) sb.Append("B ");
            if (A) sb.Append("A ");
            if (RightSR) sb.Append("RightSR ");
            if (RightSL) sb.Append("RightSL ");

            if (R) sb.Append("R ");
            if (ZR) sb.Append("ZR ");

            if (Minus) sb.Append("Minus ");
            if (Plus) sb.Append("Plus ");

            if (RStick) sb.Append("RStick ");
            if (LStick) sb.Append("LStick ");
            if (Home) sb.Append("Home ");
            if (Capture) sb.Append("Capture ");
            if (ChargingGrip) sb.Append("ChargingGrip ");

            if (Down) sb.Append("↓ ");
            if (Up) sb.Append("↑ ");
            if (Right) sb.Append("→ ");
            if (Left) sb.Append("← ");
            if (LeftSR) sb.Append("LeftSR ");
            if (LeftSL) sb.Append("LeftSL ");

            if (L) sb.Append("L ");
            if (ZL) sb.Append("ZL ");

            return sb.ToString();
        }
    }
}
