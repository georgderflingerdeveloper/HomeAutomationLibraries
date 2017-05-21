using System.Timers;

namespace TimerMockable
{
    public interface ITimer
    {
        void Start();
        void Stop();
        event ElapsedEventHandler Elapsed;
    }
}
