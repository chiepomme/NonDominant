using HidApiSharp;
using JoyConSharp;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Hidarite
{
    public enum JoyConVendorID : ushort
    {
        Nintendo = 1406,
    }

    public enum JoyConProductID : ushort
    {
        JoyConLeft = 8198,
        JoyConRight = 8199,
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _ = AAsync();

            //var device = HidDevice.Open(new() {  });

            //var connectedInfos = Enumerable.Empty<HidDeviceInfo>()
            //                   .Concat(HidDeviceInfo.Enumerate((ushort)JoyConVendorID.Nintendo, (ushort)JoyConProductID.JoyConLeft))
            //                   .Concat(HidDeviceInfo.Enumerate((ushort)JoyConVendorID.Nintendo, (ushort)JoyConProductID.JoyConRight))
            //                   .ToDictionary(inf => inf.SerialNumber, inf => inf);

            //foreach (var (serial, disconnectedJoyCon) in JoyCons.Where(kvp => !connectedInfos.ContainsKey(kvp.Key)).ToArray())
            //{
            //    JoyCons.Remove(serial);
            //    disconnectedJoyCon.Dispose();
            //    Disconnected(disconnectedJoyCon);
            //}

            //foreach (var newInfo in connectedInfos.Values.Where(inf => !JoyCons.ContainsKey(inf.SerialNumber)).ToArray())
            //{
            //    var device = HidDevice.Open(newInfo);
            //    if (device == null) continue;
            //    var joyCon = new JoyCon(device);
            //    JoyCons.Add(newInfo.SerialNumber, joyCon);
            //    Connected(joyCon);
            //}
        }

        async Task AAsync()
        {
            var info = HidDeviceInfo.Enumerate((ushort)JoyConVendorID.Nintendo, (ushort)JoyConProductID.JoyConLeft).First();
            var device = HidDevice.Open(info);
            var joycon = new JoyCon(device!);

            await Task.Delay(2000);
            joycon.SetPlayerLights(LightState.On, LightState.On, LightState.On, LightState.On);
        }
    }
}
