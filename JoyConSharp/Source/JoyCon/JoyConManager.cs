using HidApiSharp;

namespace JoyConSharp
{
    public sealed class JoyConManager : IDisposable
    {
        public readonly Dictionary<string, JoyCon> JoyCons = new();
        public event Action<JoyCon> Connected = delegate { };
        public event Action<JoyCon> Disconnected = delegate { };

        readonly CancellationTokenSource cts = new();
        Task? loopTask;

        public void Start()
        {
            loopTask = LoopAsync();
        }

        async Task LoopAsync()
        {
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    var connectedInfos = Enumerable.Empty<HidDeviceInfo>()
                                                   .Concat(HidDeviceInfo.Enumerate((ushort)JoyConVendorID.Nintendo, (ushort)JoyConProductID.JoyConLeft))
                                                   .Concat(HidDeviceInfo.Enumerate((ushort)JoyConVendorID.Nintendo, (ushort)JoyConProductID.JoyConRight))
                                                   .ToDictionary(inf => inf.SerialNumber, inf => inf);

                    foreach (var (serial, disconnectedJoyCon) in JoyCons.Where(kvp => !connectedInfos.ContainsKey(kvp.Key)).ToArray())
                    {
                        JoyCons.Remove(serial);
                        disconnectedJoyCon.Dispose();
                        Disconnected(disconnectedJoyCon);
                    }

                    foreach (var newInfo in connectedInfos.Values.Where(inf => !JoyCons.ContainsKey(inf.SerialNumber)).ToArray())
                    {
                        var device = HidDevice.Open(newInfo);
                        if (device == null) continue;
                        var joyCon = new JoyCon(device);
                        JoyCons.Add(newInfo.SerialNumber, joyCon);
                        Connected(joyCon);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[JoyConManager] Error: {e}");
                }

                await Task.Delay(100, cts.Token);
            }
        }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();

            try
            {
                loopTask?.Wait();
            }
            catch (TaskCanceledException) { }
            catch (Exception e)
            {
                Console.WriteLine($"[JoyConManager] Error: {e}");
            }

            foreach (var joyCon in JoyCons.Values)
            {
                joyCon.Dispose();
            }
        }
    }
}
