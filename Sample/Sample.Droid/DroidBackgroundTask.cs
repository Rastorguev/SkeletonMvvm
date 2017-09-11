using System.Diagnostics;
using Sample.Core;

namespace Sample.Droid
{
    internal class DroidBackgroundTask : IBackgroundTask
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