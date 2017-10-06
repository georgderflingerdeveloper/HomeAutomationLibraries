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
        void Pause();
        void Resume();
        void Stop();
        void Reset();
        bool TimedStart { set; }
        event ActivityChanged EActivityChanged;

    }
}
