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
        void Pause( TimeSpan delay );
        void Pause();
        void Resume();
        void Stop();
        void Toggle();
        void Toggle( TimeSpan delay );
        void Reset();
        event ActivityChanged EActivityChanged;

    }
}
