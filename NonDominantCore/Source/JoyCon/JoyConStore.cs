using JoyConSharp;

namespace NonDominant
{
    public class JoyConStore : IDisposable
    {
        readonly JoyConManager manager;
        readonly List<JoyCon> joyCons = new();
        readonly AppConfig config;
        readonly VirtualKeyboard keyboard = new();

        public JoyConStore(AppConfig config)
        {
            this.config = config;
            manager = new JoyConManager();
            manager.Connected += OnConnected;
            manager.Start();
        }

        void OnConnected(JoyConSharp.JoyCon joyCon)
        {
            lock (joyCons)
            {
                joyCons.Add(new JoyCon(config, keyboard, joyCon));
            }
        }

        public void Dispose()
        {
            manager.Dispose();

            foreach (var joyCon in joyCons)
            {
                joyCon.Dispose();
            }

            joyCons.Clear();
        }
    }
}
