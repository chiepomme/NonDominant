namespace JoyConSharp
{
    /// <summary>
    /// https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/bluetooth_hid_subcommands_notes.md
    /// </summary>
    public enum Subcommand : byte
    {
        None = 0x00,
        RequestDeviceInfo = 0x02,
        SetInputReportMode = 0x03,
        SetPlayerLights = 0x30,
        EnableVibration = 0x48,
    }
}
