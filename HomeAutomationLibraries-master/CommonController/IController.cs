using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnivCommonController
{
    public interface IController
    {
        void Start();
        void Stop();
        void Reset();
        void Force();
        void UnForce();
        void ForcedOn();
        void ForcedOff();
    }
}
