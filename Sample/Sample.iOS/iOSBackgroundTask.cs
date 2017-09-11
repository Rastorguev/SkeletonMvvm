using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Sample.Core;
using UIKit;

namespace Sample.iOS
{
    internal class iOSBackgroundTask : IBackgroundTask
    {
        private nint _taskId;
        private CancellationTokenSource _cancellationTokenSource;

        public bool IsRunning { get; private set; }

        public async void Start()
        {
            if (IsRunning)
            {
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();

            _taskId = UIApplication.SharedApplication.BeginBackgroundTask(() =>
            {
                Console.WriteLine("------- Background execution time expired -------");
                UIApplication.SharedApplication.EndBackgroundTask(_taskId);
            });

            IsRunning = true;

            Debug.WriteLine("------- Background task started -------");

            try
            {
                await DoWork(_cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("------- Background task canceled -------");
            }

            Debug.WriteLine("------- Background task completed -------");

            UIApplication.SharedApplication.EndBackgroundTask(_taskId);
        }

        public void Stop()
        {
            if (IsRunning)
            {
                _cancellationTokenSource?.Cancel();

                IsRunning = false;
            }

            Debug.WriteLine("------- Background task stopped -------");
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var timeRemaining = UIApplication.SharedApplication.BackgroundTimeRemaining;

                Debug.WriteLine($"------- Time Remaining: {timeRemaining} -------");

                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}