using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HidApiSharp
{
    public sealed class HidDevice : IDisposable
    {
        bool disposed;

        public static HidDevice? Open(HidDeviceInfo deviceInfo)
        {
            var devicePtr = HidApi.hid_open(deviceInfo.VenderID, deviceInfo.ProductID, deviceInfo.SerialNumber);
            if (devicePtr == IntPtr.Zero)
            {
                var error = HidApi.hid_error(IntPtr.Zero);
                var errorText = Marshal.PtrToStringUni(error);
                Debug.WriteLine($"[エラー] デバイスが開けませんでした。{errorText}");
                return null;
            }

            return new HidDevice(devicePtr, deviceInfo);
        }

        public readonly HidDeviceInfo Info;
        readonly IntPtr devicePtr;

        HidDevice(IntPtr devicePtr, HidDeviceInfo deviceInfo)
        {
            this.devicePtr = devicePtr;
            Info = deviceInfo;
        }

        public void Write(byte[] bytes)
        {
            lock (this)
            {
                if (disposed) return;
                HidApi.hid_write(devicePtr, bytes, (ulong)bytes.Length);
            }
        }

        public byte[]? Read(int? timeoutMs = null)
        {
            var bytes = new byte[1024];
            int receivedBytes;

            lock (this)
            {
                if (disposed) return null;

                if (timeoutMs != null)
                {
                    receivedBytes = HidApi.hid_read_timeout(devicePtr, bytes, (ulong)bytes.Length, timeoutMs.Value);
                }
                else
                {
                    receivedBytes = HidApi.hid_read(devicePtr, bytes, (ulong)bytes.Length);
                }
            }

            if (receivedBytes == -1)
            {
                var errorPtr = HidApi.hid_error(devicePtr);
                if (errorPtr != IntPtr.Zero)
                {
                    Debug.WriteLine($"[エラー] 読み込みに失敗しました。 {Marshal.PtrToStringUni(errorPtr)}");
                    return null;
                }
            }
            else if (receivedBytes == 0)
            {
                return null;
            }

            return bytes.AsSpan(0, receivedBytes).ToArray();
        }

        public void SetNonBlocking(bool nonblocking)
        {
            HidApi.hid_set_nonblocking(devicePtr, nonblocking ? 1 : 0);
        }

        public void Dispose()
        {
            lock (this)
            {
                if (disposed) return;
                disposed = true;
                HidApi.hid_close(devicePtr);
            }
        }
    }
}
