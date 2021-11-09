namespace JoyConSharp
{
    /// <summary>
    /// https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/bluetooth_hid_notes.md#input-reports
    /// </summary>
    public enum InputReportID : byte
    {
        StandardInputReportsWithSubCommandReply = 0x21,
        StandardFullInputReports = 0x30,
    }
}
