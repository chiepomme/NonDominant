using JoyConSharp;

namespace NonDominantConsole
{
    public static class Program
    {
        public static async Task Main()
        {
            using var manager = new JoyConManager();
            manager.Connected += OnConnected;
            manager.Disconnected += OnDisconnected;

            manager.Start();

            while (true)
            {
                await Task.Delay(1000);
            }
        }

        static void OnConnected(JoyCon joyCon)
        {
            Console.WriteLine($"Connected: {joyCon.Product} [{joyCon.DeviceInfo.Manufacturer}] ({joyCon.DeviceInfo.SerialNumber})");
            joyCon.Reported += OnJoyConReported;
            joyCon.Start();

            joyCon.Rumble(300, 1);
        }

        static void OnDisconnected(JoyCon joyCon)
        {
            Console.WriteLine($"Disconnected: {joyCon.Product} [{joyCon.DeviceInfo.Manufacturer}] ({joyCon.DeviceInfo.SerialNumber})");
        }

        static void OnJoyConReported(JoyCon joyCon, StandardInputReport report)
        {
            Console.WriteLine($"{joyCon.Product}({joyCon.DeviceInfo.SerialNumber}) {report}");
        }
    }
}
