using System.Management;
using System.Runtime.InteropServices;

namespace NonDominant
{
    static class WindowWinAPI
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        public static string? GetMainModuleFilepath(int processId)
        {
            var query = $"SELECT ExecutablePath FROM Win32_Process WHERE ProcessId = {processId}";
            using (var searcher = new ManagementObjectSearcher(query))
            using (var results = searcher.Get())
            {
                return results
                        .Cast<ManagementObject>()
                        .Select(m => m["ExecutablePath"])
                        .OfType<string>()
                        .FirstOrDefault();
            }
        }

        public static string? GetForegroundFilePath()
        {
            var foregroundHandle = GetForegroundWindow();
            GetWindowThreadProcessId(foregroundHandle, out var processId);
            return GetMainModuleFilepath(processId);
        }
    }
}
