using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace NonDominant
{
    public class Runner : IDisposable
    {
        Config? config;

        Thread? thread;
        bool disposed;

        readonly IDisposable pathChangeDisposable;

        public Runner()
        {
            pathChangeDisposable = CreateForegroundPathStream().Subscribe(foregroundPath => Restart(foregroundPath));
        }

        public void Run(Config config)
        {
            this.config = config;
            Restart(null);
        }

        void Restart(string? foregroundPath)
        {
            StopThread();

            thread = new Thread(Zero2KeyLoop);
            thread.Start(foregroundPath);
        }

        void Zero2KeyLoop(object? foregroundPathObj)
        {
            var foregroundPath = (string)foregroundPathObj!;
            var appConfig = config!
                               .AppConfigs
                               .FirstOrDefault(c => c.AppPath == foregroundPath)
                               ?? config.DefaultAppConfig;

            using var store = new JoyConStore(appConfig);
            while (!disposed)
            {
                Thread.Sleep(100);
            }
        }

        IObservable<string> CreateForegroundPathStream()
        {
            return Observable
                        .Interval(TimeSpan.FromMilliseconds(50))
                        .Select(_ => WindowWinAPI.GetForegroundWindow())
                        .Where(hwnd => hwnd != IntPtr.Zero)
                        .Select(hwnd =>
                        {
                            WindowWinAPI.GetWindowThreadProcessId(hwnd, out var processId);
                            return processId;
                        })
                        .Select(pid => WindowWinAPI.GetMainModuleFilepath(pid))
                        .Where(path => path != null)
                        .Select(path => path!)
                        .DistinctUntilChanged();
        }

        void StopThread()
        {
            if (thread != null)
            {
                disposed = true;
                thread.Join();
                disposed = false;
            }
        }

        public void Dispose()
        {
            StopThread();
            pathChangeDisposable.Dispose();
        }
    }
}
