using System.Diagnostics;

namespace NonDominant
{
    public class NonDominantCore : IDisposable
    {
        Config Config { get; }
        CancellationTokenSource Cts { get; } = new();
        Stopwatch Stopwatch { get; } = Stopwatch.StartNew();

        string? foregroundPath;

        public NonDominantCore(Config config)
        {
            Config = config;
            foregroundPath = WindowWinAPI.GetForegroundFilePath();
            _ = LoopAsync(Cts.Token).ConfigureAwait(false);
            _ = WindowSearchLoopAsync(Cts.Token).ConfigureAwait(false);
        }

        async ValueTask LoopAsync(CancellationToken ct)
        {
            try
            {
                while (true)
                {
                    var currentForegroundPath = foregroundPath;
                    var appConfig = Config!.AppConfigs.FirstOrDefault(c => c.AppPath == currentForegroundPath) ?? Config.DefaultAppConfig;
                    using var store = new JoyConStore(appConfig);

                    while (true)
                    {
                        if (Stopwatch.ElapsedMilliseconds > 1000)
                        {
                            Stopwatch.Restart();
                            if (foregroundPath != currentForegroundPath)
                            {
                                break;
                            }
                        }

                        store.Tick();
                        await Task.Delay(10, ct).ConfigureAwait(false);
                    }
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                Debug.WriteLine($"エラー: {e.GetType().Name} {e.Message}");
            }
        }

        async ValueTask WindowSearchLoopAsync(CancellationToken ct)
        {
            try
            {
                while (true)
                {
                    foregroundPath = WindowWinAPI.GetForegroundFilePath();
                    await Task.Delay(100, ct).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                Debug.WriteLine($"エラー: {e.GetType().Name} {e.Message}");
            }
        }

        public void Dispose()
        {
            Cts.Cancel();
            Cts.Dispose();
        }
    }
}
