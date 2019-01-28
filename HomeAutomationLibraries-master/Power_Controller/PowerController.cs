using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerMockable;
using CommonController;

namespace Power_Controller
{
    public class ControlTimers
    {
        public ITimer TimerAutomaticOff { get; set; } 
    }

    [Serializable]
    public class PowerStatus : Controller_State
    {
    }


    public class PowerController
    {
    }
}
