namespace JoyConSharp
{
    static class JoyConSerializer
    {
        public static byte[] SerializePlayerLights(byte packetNumber, LightState light1, LightState light2, LightState light3, LightState light4)
        {
            var lightStateByte = (byte)0;
            if (light1 == LightState.On) lightStateByte |= 1 << 3;
            if (light1 == LightState.Blink) lightStateByte |= 1 << (4 + 3);
            if (light2 == LightState.On) lightStateByte |= 1 << 2;
            if (light2 == LightState.Blink) lightStateByte |= 1 << (4 + 2);
            if (light3 == LightState.On) lightStateByte |= 1 << 1;
            if (light3 == LightState.Blink) lightStateByte |= 1 << (4 + 1);
            if (light4 == LightState.On) lightStateByte |= 1 << 0;
            if (light4 == LightState.Blink) lightStateByte |= 1 << (4 + 0);

            var bytes = new byte[12];
            bytes[0] = (byte)OutputReportID.RumbleAndSubcommand;
            bytes[1] = packetNumber;
            bytes[10] = (byte)Subcommand.SetPlayerLights;
            bytes[11] = lightStateByte;

            return bytes;
        }

        public static byte[] SerializeEnableVibration(byte packetNumber, bool enabled)
        {
            var bytes = new byte[12];
            bytes[0] = (byte)OutputReportID.RumbleAndSubcommand;
            bytes[1] = packetNumber;
            bytes[10] = (byte)Subcommand.EnableVibration;
            bytes[11] = (byte)(enabled ? 1 : 0);

            return bytes;
        }

        /// <param name="freq">0~1252</param>
        /// <param name="amp">0.0~1.0</param>
        public static byte[] SerializeRumble(byte packetNumber, int freq, double amp)
        {
            // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md
            freq = Math.Max(41, Math.Min(freq, 1252));
            amp = Math.Max(0, Math.Min(amp, 1));

            var rumbleBytes = new byte[4];

            var encodedFreq = (byte)Math.Round(Math.Log(freq / 10.0, 2) * 32);
            var hf = (ushort)((encodedFreq - 0x60) * 4);
            var lf = (byte)(encodedFreq - 0x40);

            if (freq > 80)
            {
                rumbleBytes[0] += (byte)(hf & 0xFF);
                rumbleBytes[1] += (byte)(hf >> 8);
            }

            if (freq < 634)
            {
                rumbleBytes[2] += lf;
            }

            byte encodedAmp;

            if (amp > 0.23)
            {
                encodedAmp = (byte)Math.Round(Math.Log(amp * 8.7, 2) * 32);
            }
            else if (amp > 0.12)
            {
                encodedAmp = (byte)Math.Round(Math.Log(amp * 17.0, 2) * 16);
            }
            else
            {
                encodedAmp = (byte)Math.Round(((Math.Log(amp, 2) * 32) - 96.0) / (4.0 - 2.0 * amp));
            }

            var ha = (byte)(encodedAmp * 2);
            var la = (ushort)(encodedAmp / 2 + 64);

            rumbleBytes[1] += ha;
            rumbleBytes[2] += (byte)(la >> 8);
            rumbleBytes[3] += (byte)(la & 0xFF);

            var bytes = new byte[10];
            bytes[0] = (byte)OutputReportID.RumbleOnly;
            bytes[1] = packetNumber;
            bytes[2] = rumbleBytes[0];
            bytes[3] = rumbleBytes[1];
            bytes[4] = rumbleBytes[2];
            bytes[5] = rumbleBytes[3];
            bytes[6] = rumbleBytes[0];
            bytes[7] = rumbleBytes[1];
            bytes[8] = rumbleBytes[2];
            bytes[9] = rumbleBytes[3];

            return bytes;
        }

        public static byte[] SerializeSetInputReportMode(byte packetNumber)
        {
            var bytes = new byte[12];
            bytes[0] = (byte)OutputReportID.RumbleAndSubcommand;
            bytes[1] = packetNumber;
            bytes[10] = (byte)Subcommand.SetInputReportMode;
            bytes[11] = 0x30;

            return bytes;
        }

        public static byte[] SerializeRequestDeviceInfo(byte packetNumber)
        {
            var bytes = new byte[11];
            bytes[0] = (byte)OutputReportID.RumbleAndSubcommand;
            bytes[1] = packetNumber;
            bytes[10] = (byte)Subcommand.RequestDeviceInfo;

            return bytes;
        }
    }
}
