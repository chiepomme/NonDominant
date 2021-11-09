using System.Runtime.InteropServices;

namespace HidApiSharp
{
    public class HidDeviceInfo
    {
        public static IEnumerable<HidDeviceInfo> Enumerate(ushort? vendorID, ushort? productID)
        {
            var firstDevice = HidApi.hid_enumerate(vendorID.GetValueOrDefault(0), productID.GetValueOrDefault(0));

            var device = firstDevice;
            while (device != IntPtr.Zero)
            {
                var nativeDeviceInfo = Marshal.PtrToStructure<NativeHidDeviceInfo>(device);
                yield return new HidDeviceInfo(nativeDeviceInfo);
                device = nativeDeviceInfo.NextDevice;
            }

            HidApi.hid_free_enumeration(firstDevice);
        }

        public readonly string Path;
        public readonly ushort VenderID;
        public readonly ushort ProductID;
        public readonly string SerialNumber;
        public readonly ushort ReleaseNumber;
        public readonly string Manufacturer;
        public readonly string Product;
        public readonly ushort UsagePage;
        public readonly ushort Usage;
        public readonly int InterfaceNumber;

        internal HidDeviceInfo(NativeHidDeviceInfo native)
        {
            Path = native.Path;
            VenderID = native.VenderID;
            ProductID = native.ProductID;
            SerialNumber = native.SerialNumber;
            ReleaseNumber = native.ReleaseNumber;
            Manufacturer = native.Manufacturer;
            Product = native.Product;
            UsagePage = native.UsagePage;
            Usage = native.Usage;
            InterfaceNumber = native.InterfaceNumber;
        }
    }
}
