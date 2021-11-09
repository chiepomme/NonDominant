using System.Runtime.InteropServices;
using System.Text;

namespace HidApiSharp
{
    class HidApi
    {
        const string DLL = "hidapi";

        [DllImport(DLL)]
        public static extern int hid_init();
        [DllImport(DLL)]
        public static extern int hid_exit();
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        public static extern string hid_error(IntPtr device);

        [DllImport(DLL)]
        public static extern IntPtr hid_enumerate(ushort vendor_id, ushort product_id);
        [DllImport(DLL)]
        public static extern void hid_free_enumeration(IntPtr devs);

        [DllImport(DLL)]
        public static extern int hid_get_feature_report(IntPtr device, byte[] data, ulong length);
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        public static extern int hid_get_indexed_string(IntPtr device, int string_index, StringBuilder str, ulong maxlen);
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        public static extern int hid_get_manufacturer_string(IntPtr device, StringBuilder str, ulong maxlen);
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        public static extern int hid_get_product_string(IntPtr device, StringBuilder str, ulong maxlen);
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        public static extern int hid_get_serial_number_string(IntPtr device, StringBuilder str, ulong maxlen);

        [DllImport(DLL)]
        public static extern IntPtr hid_open(ushort vendor_id, ushort product_id, [MarshalAs(UnmanagedType.LPWStr)] string serial_number);
        [DllImport(DLL, CharSet = CharSet.Ansi)]
        public static extern IntPtr hid_open_path([MarshalAs(UnmanagedType.LPStr)] string path);
        [DllImport(DLL)]
        public static extern void hid_close(IntPtr device);

        [DllImport(DLL)]
        public static extern int hid_read(IntPtr device, byte[] data, ulong length);
        [DllImport(DLL)]
        public static extern int hid_read_timeout(IntPtr dev, byte[] data, ulong length, int milliseconds);
        [DllImport(DLL)]
        public static extern int hid_send_feature_report(IntPtr device, byte[] data, ulong length);
        [DllImport(DLL)]
        public static extern int hid_set_nonblocking(IntPtr device, int nonblock);
        [DllImport(DLL)]
        public static extern int hid_write(IntPtr device, byte[] data, ulong length);
    }
}
