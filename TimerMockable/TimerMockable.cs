using System.Timers;

namespace TimerMockable
{
    public class Timer_ : ITimer
    {
        private Timer timer = new Timer();

        public Timer_(double interval)
        {
            timer.Interval = interval;
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public event ElapsedEventHandler Elapsed
        {
            add { timer.Elapsed += value; }
            remove { timer.Elapsed -= value; }
        }
    }
}
