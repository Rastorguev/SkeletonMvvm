using System.Diagnostics;
using Sample.Core;

namespace Sample.iOS
{
    internal class iOSBackgroundTask : IBackgroundTask
    {
        public bool IsRunning { get; private set; }

        public void Start()
        {
            IsRunning = true;

            Debug.WriteLine("------- Start -------");
        }

        public void Stop()
        {
            IsRunning = false;

            Debug.WriteLine("------- Stop -------");
        }
    }
}