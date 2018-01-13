using System.Timers;
using System;

namespace TimerMockable
{
    public class Timer_ : ITimer
    {

        bool _IsStarted = false;

        public void SetIntervall( double intervall )
        {
            if (intervall > 0)
            {
                timer.Interval = intervall;
            }
        }

        private Timer timer = new Timer( );

        public Timer_() {}

        public Timer_( double intervall )
        {
            if (intervall > 0)
            {
                timer.Interval = intervall;
            }
        }

        public void Start()
        {
            if (timer.Interval > 0)
            {
                timer.Start( );
                _IsStarted = true;
            }
        }

        public bool IsStarted()
        {
            return _IsStarted;
        }

        public void Stop()
        {
            if (timer.Interval > 0)
            {
                timer.Stop( );
                _IsStarted = false;
            }
        }

        public event ElapsedEventHandler Elapsed
        {
            add { timer.Elapsed += value; }
            remove { timer.Elapsed -= value; }
        }

        public void SetTime( TimeSpan time )
        {
            timer.Interval = time.TotalMilliseconds;
        }

    }
}
