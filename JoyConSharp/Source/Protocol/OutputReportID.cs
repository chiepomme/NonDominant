namespace JoyConSharp
{
    /// <summary>
    /// https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/bluetooth_hid_notes.md#output-reports
    /// </summary>
    public enum OutputReportID : byte
    {
        RumbleAndSubcommand = 0x01,
        RumbleOnly = 0x10,
    }
}
