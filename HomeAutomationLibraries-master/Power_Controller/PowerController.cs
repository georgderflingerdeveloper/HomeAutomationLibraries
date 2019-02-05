using System;
using System.Timers;
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
            TimerFinished,
            InvalidForTesting = 99
        }

        public OperationState ActualOperationState { get; set; }
    }

    public class PowerEventArgs : EventArgs
    {
        public bool TurnOn { get; set; }
        public PowerStatus Status { get; set; }
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
        ControlTimers _Timers;
        PowerParameters _Parameters;
        #endregion

        #region CONSTRUCTOR
        public PowerController(PowerParameters Parameters, ControlTimers Timers) : base()
        {
            _PowerStatus = new PowerStatus();
            _PowerEventArgs = new PowerEventArgs()
            {
                Status = _PowerStatus
            };
            _Timers = new ControlTimers()
            {
                TimerAutomaticOff = Timers.TimerAutomaticOff
            };
            _Parameters = Parameters;
            _Timers.TimerAutomaticOff.Elapsed += TimerAutomaticOffElapsed;

            SetOperationState(PowerStatus.OperationState.Idle);
        }

        #endregion

        #region OVERRIDE
        override protected void ControllerStart()
        {
            SetControllerState(ControllerInformer.ControllerState.ControllerIsOn);
            SetOperationState(PowerStatus.OperationState.TimerActive);
            _PowerEventArgs.TurnOn = true;
            _Timers.TimerAutomaticOff.SetTime(_Parameters.DurationPowerOn);
            _Timers.TimerAutomaticOff.Stop();
            _Timers.TimerAutomaticOff.Start();
            Update();
        }

        override protected void Update()
        {
            _PowerEventArgs.Status.ActualControllerState = CommonEventArgs.Informer.ActualControllerState;
            EActivityChanged?.Invoke( this, _PowerEventArgs );
        }
        #endregion

        #region EVENTHANDLERS
        private void TimerAutomaticOffElapsed(object sender, ElapsedEventArgs e)
        {
            _PowerEventArgs.TurnOn = false;
            Update();
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
