using System.Timers;
using System;

namespace TimerMockable
{
    public interface ITimer
    {
        #region Events
        event ElapsedEventHandler Elapsed;
        #endregion

        #region Methods
        bool IsStarted();

        void SetIntervall( double intervall );

        void Start();

        void Stop();

        void SetTime( TimeSpan time );
        #endregion
    }
}
