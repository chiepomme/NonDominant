using System.Diagnostics;

namespace HidApiSharp
{
    public sealed class HidDevice : IDisposable
    {
        readonly ReaderWriterLockSlim disposeLock = new();
        bool disposed;

        public static HidDevice? Open(HidDeviceInfo deviceInfo)
        {
            var devicePtr = HidApi.hid_open(deviceInfo.VenderID, deviceInfo.ProductID, deviceInfo.SerialNumber);
            if (devicePtr == IntPtr.Zero)
            {
                Debug.WriteLine($"[エラー] デバイスが開けませんでした。");
                return null;
            }

            //var error = HidApi.hid_error(devicePtr);
            //if (error != null)
            //{
            //    Debug.WriteLine($"[エラー] デバイスが開けませんでした。 {error}");
            //}

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
            disposeLock.EnterReadLock();
            try
            {
                if (disposed) return;
                HidApi.hid_write(devicePtr, bytes, (ulong)bytes.Length);
            }
            finally
            {
                disposeLock.ExitReadLock();
            }
        }

        public byte[]? Read(int? timeoutMs = null)
        {
            var bytes = new byte[1024];
            int receivedBytes;

            disposeLock.EnterReadLock();
            try
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
            finally
            {
                disposeLock.ExitReadLock();
            }

            if (receivedBytes == -1)
            {
                return null;
                //var error = HidApi.hid_error(devicePtr);
                //if (error != null)
                //{
                //    Debug.WriteLine($"[エラー] 読み込みに失敗しました。 {error}");
                //    return null;
                //}
            }
            else if (receivedBytes == 0)
            {
                return null;
            }

            return bytes.AsSpan(0, receivedBytes).ToArray();
        }

        public void Dispose()
        {
            disposeLock.EnterWriteLock();
            try
            {
                if (disposed) return;
                disposed = true;
                HidApi.hid_close(devicePtr);
            }
            finally
            {
                disposeLock.ExitWriteLock();
            }
        }
    }
}
