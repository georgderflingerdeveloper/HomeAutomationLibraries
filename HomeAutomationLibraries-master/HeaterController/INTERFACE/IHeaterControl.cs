using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationHeater.INTERFACE
{
    interface IHeaterControl
    {
        void Start();
        void DelayedPause();
        void Pause();
        void Resume();
        void Stop();
        void Toggle();
        void DelayedToggle();
        void Reset();
        void UpdateParameters( HeaterParameters Parameters );
        HeaterStatus GetStatus();
        event ActivityChanged EActivityChanged;

    }
}
