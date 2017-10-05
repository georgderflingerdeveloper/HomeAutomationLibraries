using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeaterController.INTERFACE
{
    interface IHeaterControl
    {
        void Start();
        void Pause();
        void Resume();
        void Stop();
        event ActivityChanged EActivityChanged;

    }
}
