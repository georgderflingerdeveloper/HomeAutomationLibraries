using System.Timers;
using System;

namespace TimerMockable
{
    /// <summary>
    /// Defines the <see cref="ITimer" />
    /// </summary>
    public interface ITimer
    {
        #region Events

        /// <summary>
        /// Defines the Elapsed
        /// </summary>
        event ElapsedEventHandler Elapsed;

        #endregion

        #region Methods

        /// <summary>
        /// The IsStarted
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        bool IsStarted();

        /// <summary>
        /// The SetIntervall
        /// </summary>
        /// <param name="intervall">The <see cref="double"/></param>
        void SetIntervall( double intervall );

        /// <summary>
        /// The Start
        /// </summary>
        void Start();

        /// <summary>
        /// The Stop
        /// </summary>
        void Stop();
        void SetTime( TimeSpan time );
        #endregion
    }
}
