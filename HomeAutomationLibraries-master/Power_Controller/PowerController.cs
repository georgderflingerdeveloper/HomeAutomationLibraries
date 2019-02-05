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

    public class PowerController  : CommonController, IController
    {
        #region DECLARATION
        PowerStatus _PowerStatus;
        PowerEventArgs _PowerEventArgs;
        #endregion

        #region CONSTRUCTOR
        public PowerController(PowerParameters Parameters, ControlTimers Timers) : base()
        {
            _PowerStatus    = new PowerStatus();
            _PowerEventArgs = new PowerEventArgs();

            SetOperationState(PowerStatus.OperationState.Idle);
        }
        #endregion

        #region OVERRIDE
        override protected void ControllerStart()
        {
            Turn(GeneralConstants.ON);
            SetControllerState(ControllerInformer.ControllerState.ControllerIsOn);
            SetOperationState(PowerStatus.OperationState.TimerActive);
            _PowerEventArgs.TurnOn = true;
            Update();
        }

        override protected void Update()
        {
            EActivityChanged?.Invoke( this, _PowerEventArgs );
        }
        #endregion

        #region PRIVATE
        void SetOperationState(PowerStatus.OperationState operationState)
        {
            _PowerStatus.ActualOperationState = operationState;
        }
        #endregion

        #region NEW
        public new event ActivityChanged EActivityChanged;
        #endregion
    }


}
