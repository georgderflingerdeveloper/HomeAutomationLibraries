using System.Timers;
using System;

namespace TimerMockable
{
    public interface ITimer
    {
        void Start();
        void Stop();
        void SetTime( TimeSpan time );
        event ElapsedEventHandler Elapsed;
    }
}
