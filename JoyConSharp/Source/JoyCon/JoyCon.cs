using HidApiSharp;

namespace JoyConSharp
{
    public sealed class JoyCon : IDisposable
    {
        readonly PacketCounter packetCounter = new();
        readonly HidDevice hidDevice;

        readonly CancellationTokenSource cts = new();

        public HidDeviceInfo DeviceInfo => hidDevice.Info;
        public JoyConVendorID Vendor => (JoyConVendorID)DeviceInfo.VenderID;
        public JoyConProductID Product => (JoyConProductID)DeviceInfo.ProductID;

        public event Action<JoyCon, StandardInputReport> Reported = delegate { };

        public JoyCon(HidDevice hidDevice)
        {
            this.hidDevice = hidDevice;
            hidDevice.SetNonBlocking(true);
            SetInputReportMode();
        }

        public void Start()
        {
            _ = LoopAsync();
        }

        async Task LoopAsync()
        {
            while (true)
            {
                try
                {
                    var report = hidDevice.Read();
                    if (report == null)
                    {
                        await Task.Delay(10, cts.Token).ConfigureAwait(false);
                        continue;
                    }

                    switch ((InputReportID)report[0])
                    {
                        case InputReportID.StandardFullInputReports:
                        {
                            var rep = new StandardInputReport(report);
                            Reported(this, rep);
                            break;
                        }
                        case InputReportID.StandardInputReportsWithSubCommandReply:
                        {
                            var rep = new StandardInputReport(report);
                            Reported(this, rep);

                            var replyTo = (Subcommand)report[14];
                            switch (replyTo)
                            {
                                case Subcommand.RequestDeviceInfo:
                                    break;
                            }
                            break;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[JoyCon] Error: {e}");
                }
            }
        }

        public void SetPlayerLights(LightState light1, LightState light2, LightState light3, LightState light4)
        {
            var bytes = JoyConSerializer.SerializePlayerLights(packetCounter.Get(), light1, light2, light3, light4);
            hidDevice.Write(bytes);
        }

        public void SetPlayerLights(IList<LightState> lights)
        {
            SetPlayerLights(lights[0], lights[1], lights[2], lights[3]);
        }

        public void EnableVibration(bool enabled)
        {
            var bytes = JoyConSerializer.SerializeEnableVibration(packetCounter.Get(), enabled);
            hidDevice.Write(bytes);
        }

        public void SetInputReportMode()
        {
            var bytes = JoyConSerializer.SerializeSetInputReportMode(packetCounter.Get());
            hidDevice.Write(bytes);
        }

        /// <param name="freq">0~1252</param>
        /// <param name="amp">0.0~1.0</param>
        public void Rumble(int freq, double amp)
        {
            var bytes = JoyConSerializer.SerializeRumble(packetCounter.Get(), freq, amp);
            hidDevice.Write(bytes);
        }

        public void RequestDeviceInfo()
        {
            var bytes = JoyConSerializer.SerializeRequestDeviceInfo(packetCounter.Get());
            hidDevice.Write(bytes);
        }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();

            hidDevice.Dispose();
        }
    }
}
