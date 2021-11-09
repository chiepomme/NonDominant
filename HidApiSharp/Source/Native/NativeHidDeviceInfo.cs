#pragma warning disable 0649
using System.Runtime.InteropServices;

namespace HidApiSharp
{
    struct NativeHidDeviceInfo
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string Path;
        public ushort VenderID;
        public ushort ProductID;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string SerialNumber;
        public ushort ReleaseNumber;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Manufacturer;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Product;
        public ushort UsagePage;
        public ushort Usage;
        public int InterfaceNumber;
        public IntPtr NextDevice;
    }
}
