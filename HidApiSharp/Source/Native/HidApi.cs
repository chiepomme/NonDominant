using System.Runtime.InteropServices;
using System.Text;

namespace HidApiSharp
{
    public class HidApi
    {
        const string DLL = "hidapi";

        /// <summary>
        /// Initialize the HIDAPI library.
        /// </summary>
        /// <remarks>
        /// This function initializes the HIDAPI library.
        /// Calling it is not strictly necessary, as it will be called automatically by
        /// <see cref="hid_enumerate(ushort, ushort)"/> and
        /// any of <see cref="hid_open(ushort, ushort, string)"/> functions if it is needed.
        /// This function should be called at the beginning of execution however,
        /// if there is a chance of HIDAPI handles being opened by different threads simultaneously.
        /// </remarks>
        /// <returns>
        /// This function returns 0 on success and -1 on error.
        /// </returns>
        [DllImport(DLL)]
        public static extern int hid_init();

        /// <summary>
        /// Finalize the HIDAPI library.
        /// </summary>
        /// <remarks>
        /// This function frees all of the static data associated with HIDAPI.
        /// It should be called at the end of execution to avoid memory leaks.
        /// </remarks>
        /// <returns>This function returns 0 on success and -1 on error.</returns>
        [DllImport(DLL)]
        public static extern int hid_exit();

        /// <summary>
        /// Get a string describing the last error which occurred.
        /// </summary>
        /// <remarks>
        /// Whether a function sets the last error is noted in its documentation.
        /// These functions will reset the last error to NULL before their execution.
        /// 
        /// Strings returned from <see cref="hid_error(IntPtr)"/> must not be freed by the user!
        /// 
        /// This function is thread-safe, and error messages are thread-local.
        /// </remarks>
        /// <param name="device">
        /// A device handle returned from <see cref="hid_open(ushort, ushort, string)"/>,
        /// or NULL to get the last non-device-specific error (e.g. for errors in <see cref="hid_open(ushort, ushort, string)"/> itself).
        /// </param>
        /// <returns>
        /// This function returns a string containing the last error
        /// which occurred or NULL if none has occurred.
        /// </returns>
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        //[return: MarshalAs(UnmanagedType.LPWStr)]
        public static extern IntPtr hid_error(IntPtr device);

        /// <summary>
        /// Get a runtime version of the library.
        /// </summary>
        /// <returns>
        /// Pointer to statically allocated struct, that contains version.
        /// <seealso cref="HidApiVersion"/>
        /// </returns>
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        public static extern IntPtr hid_version(IntPtr device);

        /// <summary>
        /// Get a runtime version of the library.
        /// </summary>
        /// <returns>Pointer to statically allocated string, that contains version string.</returns>
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string hid_version_str(IntPtr device);

        /// <summary>
        /// Enumerate the HID Devices.
        /// </summary>
        /// <remarks>
        /// This function returns a linked list of all the HID devices
        /// attached to the system which match vendor_id and product_id.
        /// If <paramref name="vendor_id"/> is set to 0 then any vendor matches.
        /// If <paramref name="product_id"/> is set to 0 then any product matches.
        /// If <paramref name="vendor_id"/> and <paramref name="product_id"/> are both set to 0,
        /// then all HID devices will be returned.
        /// </remarks>
        /// <param name="vendor_id">The Vendor ID (VID) of the types of device to open.</param>
        /// <param name="product_id">The Product ID (PID) of the types of device to open.</param>
        /// <returns>
        /// This function returns a pointer to a linked list of type struct <see cref="NativeHidDeviceInfo"/>,
        /// containing information about the HID devices attached to the system, or NULL in the case of failure.
        /// Free this linked list by calling <see cref="hid_free_enumeration(IntPtr)"/>.
        /// </returns>
        [DllImport(DLL)]
        public static extern IntPtr hid_enumerate(ushort vendor_id, ushort product_id);

        /// <summary>
        /// Free an enumeration Linked List
        /// </summary>
        /// 
        /// <param name="devs">Pointer to a list of struct_device returned from <see cref="hid_enumerate(ushort, ushort)"/>.</param>
        [DllImport(DLL)]
        public static extern void hid_free_enumeration(IntPtr devs);

        /// <summary>
        /// Get a feature report from a HID device.
        /// </summary>
        /// <remarks>
        /// A device handle returned from hid_open().
        /// Set the first byte of <paramref name="data"/> to the Report ID of thereport to be read.
        /// Make sure to allow space for this extra byte in <paramref name="data"/>. Upon return, the first byte will
        /// still contain the Report ID, and the report data will start in <paramref name="data"/>[1].
        /// </remarks>
        /// <param name="device">A device handle returned from <see cref="hid_open(ushort, ushort, string)"/>.</param>
        /// <param name="data">
        /// A buffer to put the read data into, including the Report ID.
        /// Set the first byte of <paramref name="data"/> to the Report ID of the report to be read,
        /// or set it to zero if your device does not use numbered reports.
        /// </param>
        /// <param name="length">
        /// The number of bytes to read, including an extra byte for the report ID.
        /// The buffer can be longer than the actual report.
        /// </param>
        /// <returns>
        /// This function returns the number of bytes read plus one
        /// for the report ID(which is still in the first byte), or -1 on error.
        /// </returns>
        [DllImport(DLL)]
        public static extern int hid_get_feature_report(IntPtr device, byte[] data, ulong length);

        /// <summary>
        /// Get a input report from a HID device.
        /// </summary>
        /// <remarks>
        /// Since version 0.10.0, HID_API_VERSION >= HID_API_MAKE_VERSION(0, 10, 0)
        /// Set the first byte of <paramref name="data"/> to the Report ID of the report to be read.
        /// Make sure to allow space for this extra byte in <paramref name="data"/>.
        /// Upon return, the first byte will still contain the Report ID,
        /// and the report data will start in <paramref name="data"/>[1].
        /// </remarks>
        /// <param name="device">A device handle returned from <see cref="hid_open(ushort, ushort, string)"/>.</param>
        /// <param name="data">
        /// A buffer to put the read data into, including the Report ID.
        /// Set the first byte of <paramref name="data"/> to the Report ID of the report to be read,
        /// or set it to zero if your device does not use numbered reports.
        /// <param name="length">
        /// The number of bytes to read, including an extra byte for the report ID.
        /// The buffer can be longer than the actual report.
        /// </param>
        /// <returns>
        /// This function returns the number of bytes read plus one
        /// for the report ID(which is still in the first byte), or -1 on error.
        /// </returns>
        [DllImport(DLL)]
        public static extern int hid_get_input_report(IntPtr device, byte[] data, ulong length);

        /// <summary>
        /// Get a string from a HID device, based on its string index.
        /// </summary>
        /// <param name="device">A device handle returned from <see cref="hid_open(ushort, ushort, string)"/>.</param>
        /// <param name="string_index">The index of the string to get.</param>
        /// <param name="str">A wide string buffer to put the data into.</param>
        /// <param name="maxlen">The length of the buffer in multiples of wchar_t.</param>
        /// <returns>This function returns 0 on success and -1 on error.</returns>
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        public static extern int hid_get_indexed_string(IntPtr device, int string_index, StringBuilder str, ulong maxlen);

        /// <summary>
        /// Get The Manufacturer String from a HID device.

        /// </summary>
        /// <param name="string_index">The index of the string to get.</param>
        /// <param name="str">A wide string buffer to put the data into.</param>
        /// <param name="maxlen">The length of the buffer in multiples of wchar_t.</param>
        /// <returns>This function returns 0 on success and -1 on error.</returns>
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        public static extern int hid_get_manufacturer_string(IntPtr device, StringBuilder str, ulong maxlen);

        /// <summary>
        /// Get The Product String from a HID device.
        /// </summary>
        /// <param name="string_index">The index of the string to get.</param>
        /// <param name="str">A wide string buffer to put the data into.</param>
        /// <param name="maxlen">The length of the buffer in multiples of wchar_t.</param>
        /// <returns>This function returns 0 on success and -1 on error.</returns>
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        public static extern int hid_get_product_string(IntPtr device, StringBuilder str, ulong maxlen);

        /// <summary>
        /// Get The Serial Number String from a HID device.
        /// </summary>
        /// <param name="string_index">The index of the string to get.</param>
        /// <param name="str">A wide string buffer to put the data into.</param>
        /// <param name="maxlen">The length of the buffer in multiples of wchar_t.</param>
        /// <returns>This function returns 0 on success and -1 on error.</returns>
        [DllImport(DLL, CharSet = CharSet.Unicode)]
        public static extern int hid_get_serial_number_string(IntPtr device, StringBuilder str, ulong maxlen);

        /// <summary>
        /// Open a HID device using a Vendor ID (VID), Product ID (PID) and optionally a serial number.
        /// </summary>
        /// <remarks>
        /// If <paramref name="serial_number"/> is NULL, the first device with the specified VID and PID is opened.
        /// This function sets the return value of <see cref="hid_error(IntPtr)"/>.
        /// </remarks>
        /// <param name="vendor_id">The Vendor ID (VID) of the device to open.</param>
        /// <param name="product_id">The Product ID (PID) of the device to open.</param>
        /// <param name="serial_number">The Serial Number of the device to open (Optionally NULL).</param>
        /// <returns></returns>
        [DllImport(DLL)]
        public static extern IntPtr hid_open(ushort vendor_id, ushort product_id, [MarshalAs(UnmanagedType.LPWStr)] string serial_number);

        /// <summary>
        /// Open a HID device by its path name.
        /// </summary>
        /// <remarks>
        /// The path name be determined by calling <see cref="hid_enumerate(ushort, ushort)"/>,
        /// or a platform-specific path name can be used(eg: /dev/hidraw0 on Linux).
        /// This function sets the return value of <see cref="hid_error(IntPtr)"/>.
        /// </remarks>
        /// <param name="path">The path name of the device to open</param>
        /// <returns>This function returns a pointer to a #hid_device object on success or NULL on failure.</returns>
        [DllImport(DLL, CharSet = CharSet.Ansi)]
        public static extern IntPtr hid_open_path([MarshalAs(UnmanagedType.LPStr)] string path);

        /// <summary>
        /// Close a HID device.
        /// </summary>
        /// <remarks>
        /// This function sets the return value of hid_error().
        /// </remarks>
        /// <param name="device">A device handle returned from <see cref="hid_open(ushort, ushort, string)"/>.</param>
        [DllImport(DLL)]
        public static extern void hid_close(IntPtr device);

        /// <summary>
        /// Read an Input report from a HID device.
        /// </summary>
        /// <remarks>
        /// Input reports are returned to the host through the INTERRUPT IN endpoint.
        /// The first byte will contain the Report number if the device uses numbered reports.
        /// This function sets the return value of <see cref="hid_error(IntPtr)"/>.
        /// </remarks>
        /// <param name="device">A device handle returned from <see cref="hid_open(ushort, ushort, string)"/>.</param>
        /// <param name="data">A buffer to put the read data into.</param>
        /// <param name="length">
        /// The number of bytes to read.
        /// For devices with multiple reports, make sure to read an extra byte for the report number.
        /// </param>
        /// <returns>
        /// This function returns the actual number of bytes read and -1 on error.
        /// If no packet was available to be read and the handle is in non-blocking mode,
        /// this function returns 0.
        /// </returns>
        [DllImport(DLL)]
        public static extern int hid_read(IntPtr device, byte[] data, ulong length);

        /// <summary>
        /// Read an Input report from a HID device with timeout.
        /// </summary>
        /// <remarks>
        /// Input reports are returned to the host through the INTERRUPT IN endpoint.
        /// The first byte will contain the Report number if the device uses numbered reports.
        /// This function sets the return value of <see cref="hid_error(IntPtr)"/>.
        /// </remarks>
        /// <param name="device">A device handle returned from <see cref="hid_open(ushort, ushort, string)"/>.</param>
        /// <param name="data">A buffer to put the read data into.</param>
        /// <param name="length">
        /// The number of bytes to read.
        /// For devices with multiple reports, make sure to read an extra byte for the report number.
        /// </param>
        /// <param name="milliseconds">timeout in milliseconds or -1 for blocking wait.</param>
        /// <returns>
        /// This function returns the actual number of bytes read and -1 on error.
        /// If no packet was available to be read and the handle is in non-blocking mode,
        /// this function returns 0.
        /// </returns>
        [DllImport(DLL)]
        public static extern int hid_read_timeout(IntPtr dev, byte[] data, ulong length, int milliseconds);

        /// <summary>
        /// Send a Feature report to the device.
        /// </summary>
        /// <remarks>
        /// Feature reports are sent over the Control endpoint as a Set_Report transfer.
        /// The first byte of <paramref name="data"/> must contain the Report ID.
        /// For devices which only support a single report, this must be set to 0x0.
        /// The remaining bytes contain the report data.
        /// Since the Report ID is mandatory, calls to <see cref="hid_send_feature_report(IntPtr, byte[], ulong)"/>
        /// will always contain one more byte than the report contains.
        /// For example, if a hid report is 16 bytes long, 17 bytes must be passed to hid_send_feature_report():
        /// the Report ID (or 0x0, for devices which do not use numbered reports), followed by the report data (16 bytes).
        /// In this example, the length passed in would be 17.
        /// 
        /// This function sets the return value of <see cref="hid_error(IntPtr)"/>.
        /// </remarks>
        /// <param name="device">A device handle returned from <see cref="hid_open(ushort, ushort, string)"/>.</param>
        /// <param name="data">The data to send, including the report number as the first byte.</param>
        /// <param name="length">The length in bytes of the data to send, including the report number.</param>
        /// <returns>This function returns the actual number of bytes written and -1 on error.</returns>
        [DllImport(DLL)]
        public static extern int hid_send_feature_report(IntPtr device, byte[] data, ulong length);

        /// <summary>
        /// Set the device handle to be non-blocking.
        /// </summary>
        /// <remarks>
        /// In non-blocking mode calls to <see cref="hid_read(IntPtr, byte[], ulong)"/> will return immediately
        /// with a value of 0 if there is no data to be read.
        /// In blocking mode, <see cref="hid_read(IntPtr, byte[], ulong)"/> will wait(block)
        /// until there is data to read before returning.
        /// 
        /// Nonblocking can be turned on and off at any time.
        /// </remarks>
        /// <param name="device">A device handle returned from <see cref="hid_open(ushort, ushort, string)"/>.</param>
        /// <param name="nonblock">
        /// enable or not the nonblocking reads
        /// - 1 to enable nonblocking
        /// - 0 to disable nonblocking.
        /// </param>
        /// <returns>This function returns 0 on success and -1 on error.</returns>
        [DllImport(DLL)]
        public static extern int hid_set_nonblocking(IntPtr device, int nonblock);

        /// <summary>
        /// Write an Output report to a HID device.
        /// </summary>
        /// <remarks>
        /// The first byte of <paramref name="data"/> must contain the Report ID.
        /// For devices which only support a single report, this must be set to 0x0.
        /// The remaining bytes contain the report data.
        /// Since the Report ID is mandatory, calls to <see cref="hid_write(IntPtr, byte[], ulong)"/>
        /// will always contain one more byte than the report contains.
        /// For example, if a hid report is 16 bytes long, 17 bytes must be passed to <see cref="hid_write(IntPtr, byte[], ulong)"/>,
        /// the Report ID(or 0x0, for devices with a single report), followed by the report data(16 bytes).
        /// In this example, the length passed in would be 17.
        /// <see cref="hid_write(IntPtr, byte[], ulong)"/> will send the data on the first OUT endpoint, if one exists.
        /// If it does not, it will send the data through the Control Endpoint (Endpoint 0).
        /// This function sets the return value of <see cref="hid_error(IntPtr)"/>.
        /// </remarks>
        /// <param name="device">A device handle returned from <see cref="hid_open(ushort, ushort, string)"/>.</param>
        /// <param name="data">The data to send, including the report number as the first byte.</param>
        /// <param name="length">The length in bytes of the data to send.</param>
        /// <returns>This function returns the actual number of bytes written and -1 on error.</returns>
        [DllImport(DLL)]
        public static extern int hid_write(IntPtr device, byte[] data, ulong length);
    }
}
