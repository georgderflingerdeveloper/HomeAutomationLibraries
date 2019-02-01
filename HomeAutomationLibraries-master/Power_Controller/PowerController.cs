using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerMockable;
using UnivCommonController;
using HardConfig.COMMON;

namespace Power_Controller
{
    public class ControlTimers
    {
        public ITimer TimerAutomaticOff { get; set; } 
    }

    [Serializable]
    public class PowerStatus : ControllerInformer
    {
        public enum OperationState
        {
            Idle,
            TimerActive,
            TimerFinished
        }

        public OperationState ActualOperationState { get; set; }
    }

    public class PowerEventArgs : EventArgs
    {
        public bool TurnOn { get; set; }
    }

    [Serializable]
    public class PowerParameters
    {
        public TimeSpan DurationPowerOn { get; set; }
    }

    public delegate void ActivityChanged(object sender, PowerEventArgs e);


    public class PowerController  : CommonController
    {
        PowerStatus _PowerStatus;

        public PowerController(PowerParameters Parameters, ControlTimers Timers) : base()
        {
            _PowerStatus = new PowerStatus();
            SetOperationState(PowerStatus.OperationState.Idle);
        }

        override protected void ControllerStart()
        {
            Turn(GeneralConstants.ON);
            SetControllerState(ControllerInformer.ControllerState.ControllerIsOn);
            SetOperationState(PowerStatus.OperationState.TimerActive);
            Update();
        }

        void SetOperationState(PowerStatus.OperationState operationState)
        {
            _PowerStatus.ActualOperationState = operationState;
        }
    }


}
