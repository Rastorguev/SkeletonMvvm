namespace Sample.Core
{
    public interface IBackgroundTask
    {
        bool IsRunning { get; }
        void Start();
        void Stop();
    }
}