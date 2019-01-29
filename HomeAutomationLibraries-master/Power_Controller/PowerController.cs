using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerMockable;
using UnivCommonController;

namespace Power_Controller
{
    public class ControlTimers
    {
        public ITimer TimerAutomaticOff { get; set; } 
    }

    [Serializable]
    public class PowerStatus : ControllerInformer
    {
    }

    public class PowerEventArgs : EventArgs
    {
        public bool TurnOn { get; set; }
    }

    public delegate void ActivityChanged(object sender, PowerEventArgs e);




    public class PowerController
    {
    }
}
