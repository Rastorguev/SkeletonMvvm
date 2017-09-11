using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Sample.Core;
using Debug = System.Diagnostics.Debug;
using OperationCanceledException = System.OperationCanceledException;

namespace Sample.Droid
{
    [Service]
    internal class DroidBackgroundTask : IBackgroundTask
    {
        public bool IsRunning { get; private set; }

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }

            Application.Context.StartService(new Intent(Application.Context, typeof(BackgroundService)));

            IsRunning = true;
        }

        public void Stop()
        {
            if (!IsRunning)
            {
                return;
            }

            Application.Context.StopService(new Intent(Application.Context, typeof(BackgroundService)));

            IsRunning = false;
        }
    }

    [Service]
    public class BackgroundService : Service
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private DateTime? _startTime;

        public BackgroundService()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _startTime = DateTime.Now;

            Task.Factory.StartNew(async () =>
            {
                try
                {
                    await DoWork(_cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    Debug.WriteLine("------- Background task canceled -------");
                }
            });

            Debug.WriteLine("------- Background task started -------");

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();

            base.OnDestroy();

            Console.WriteLine("------- Background task is stopped ------- ");
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (_startTime != null)
                {
                    var now = DateTime.Now;
                    var elapsed = now - _startTime.Value;

                    Console.WriteLine($"------- Background task is running {elapsed.TotalSeconds} for seconds------- ");
                }
                else
                {
                    Console.WriteLine("------- Background task is running ------- ");
                }

                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}