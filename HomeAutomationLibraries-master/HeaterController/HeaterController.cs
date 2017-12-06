using HardConfig.COMMON;
using HomeAutomationHeater.INTERFACE;
using System;
using System.Timers;
using TimerMockable;
using Signalsequencer;

namespace HomeAutomationHeater
{
    public class ControlTimers
    {
        public ITimer TimerOnOff  { get; set; } // timer for turning heating system on
        public ITimer TimerLow    { get; set; } // timer for changing itensity
        public ITimer TimerMiddle { get; set; }
        public ITimer TimerHigh   { get; set; }
        public ITimer TimerPwm    { get; set; } // worker timer for pwm job
        public ITimer TimerSignal { get; set; } // timer for indication signal
    }

    [Serializable]
    public class HeaterStatus
    {
        public enum ControllerState
        {
            ControllerIsOff,
            ControllerIsOn,
            ControllerIsExpectingPause,
            ControllerIsPaused,
            InvalidForTesting = 99
        }

        public ControllerState ActualControllerState { get; set; }

        public  int ActualSignalisationCounts { get; set; }

        public enum OperationState
        {
            Idle,
            RegularOperation,
            PwmIsWorking,
            TemperatureControl,
            Boosting,
            Defrosting,
            InvalidForTesting = 99
        }

        public OperationState ActualOperationState { get; set; }

        public enum PwmState
        {
            Idle,
            Low,
            Medium,
            High,
            PwmInvalidForTesting = 99
        }

        public PwmState ActualPwmState { get; set; }


        public enum InformationAction
        {
            None,
            TurningOn,
            ItensityChanging,
            TurningOff,
            Pausing,
            Finished,
            InvalidForTesting = 99
        }
        public InformationAction ActualActionInfo { get; set; }

    }

    [Serializable]
    public class HeaterMode
    {
        public enum OperationMode
        {
            Permanent,
            PulseWidthModulation,
            TemperatureControlHystersis
        }

        public OperationMode SelectedOperationMode { get; set; }

    }

    [Serializable]
    public class HeaterParameters
    {
        public float SetTemperatureDay { get; set; }
        public float SetTemperatureNightLowering { get; set; }
        public float SetTemperatureEstimationTolerance { get; set; }
        public TimeSpan CmdDurationForTurningStartingStopping { get; set; }
        public TimeSpan CmdDurationForItensityChange { get; set; }
        public TimeSpan CmdDurationForActivatingPause { get; set; }
        public TimeSpan SignalDurationSignalisation { get; set; }
        public TimeSpan SignalDurationOn { get; set; }
        public TimeSpan SignalDurationOff { get; set; }
        public TimeSpan SignalDurationLowOn { get; set; }
        public TimeSpan SignalDurationLowOff { get; set; }
        public TimeSpan SignalDurationMiddleOn { get; set; }
        public TimeSpan SignalDurationMiddleOff { get; set; }
        public TimeSpan SignalDurationHighOn { get; set; }
        public TimeSpan SignalDurationHighOff { get; set; }
        public TimeSpan SignalDurationVariableOn { get; set; }
        public TimeSpan SignalDurationVariableOff { get; set; }
        public TimeSpan SignalDurationBoosting { get; set; }
        public TimeSpan DurationDelayPause { get; set; }
    }

    public class HeaterControllerEventArgs : EventArgs
    {
        public bool TurnOn { get; set; }
        public HeaterStatus Status { get; set; }
    }

    public delegate void ActivityChanged( object sender, HeaterControllerEventArgs e );

    public class HeaterController : IHeaterControl
    {
        #region DECLARATIONES
        bool _CommandTurnOn;
        bool _ToggleController;
        ITimer _DelayToggelingController;
        ITimer _DelayPause;
        HeaterParameters _Parameters;
        protected HeaterStatus _Status;
        protected HeaterControllerEventArgs HeaterEvArgs;
        #endregion

        #region CONSTRUCTOR
        public HeaterController( HeaterParameters Parameters, ITimer DelayControllerPause, ITimer DelayToggelingController )
        {
            _Parameters = Parameters;
            HeaterEvArgs = new HeaterControllerEventArgs
            {
                Status = new HeaterStatus( )
            };
            _Status = HeaterEvArgs.Status;
            _DelayToggelingController = DelayToggelingController;
            _DelayToggelingController.Elapsed += DelayToggelingControllerElapsed;
            _DelayPause = DelayControllerPause;
            _DelayPause.Elapsed += DelayPauseElapsed;
        }
        #endregion

        #region EVENTHANDLERS
        private void DelayToggelingControllerElapsed( object sender, ElapsedEventArgs e )
        {
            ToggleController( );
        }

        private void DelayPauseElapsed( object sender, ElapsedEventArgs e )
        {
            ControllerPause( );
        }
        #endregion

        #region PROPERTIES
        #endregion

        #region INTERFACE_IMPLEMENTATION
        public void Start()
        {
            ControllerStart( );
        }

        public void DelayedPause()
        {
            InformerExpectingPause( );
            ActivateTimer( _Parameters.DurationDelayPause, _DelayPause );
        }

        public void Pause()
        {
            ControllerPause( );
        }

        public void Resume()
        {
            if (( HeaterEvArgs.Status.ActualControllerState == HeaterStatus.ControllerState.ControllerIsPaused ) ||
                ( HeaterEvArgs.Status.ActualControllerState == HeaterStatus.ControllerState.ControllerIsExpectingPause ))
            {
                ControllerStart( );
            }
        }

        public void Stop()
        {
            ControllerStop( );
        }

        public void Reset()
        {
        }

        public void Confirm()
        {
        }

        public void Toggle()
        {
            ToggleController( );
        }

        public void DelayedToggle()
        {
            InformerTurningOn( );
            ActivateTimer( _Parameters.CmdDurationForTurningStartingStopping, _DelayToggelingController );
        }

        public void UpdateParameters( HeaterParameters Parameters )
        {
            _Parameters = Parameters;
        }

        public HeaterStatus GetStatus()
        {
            return _Status;
        }

        public void  SetStatus( HeaterStatus status )
        {
            _Status = status;
        }

        public event ActivityChanged EActivityChanged;
        #endregion

        #region PRIVATE
        void ActivateTimer( TimeSpan delay, ITimer Timer_ )
        {
            Timer_.SetTime( delay );
            Timer_.Stop( );
            Timer_.Start( );
        }

        void InformerTurningOn()
        {
            HeaterEvArgs.Status.ActualActionInfo = HeaterStatus.InformationAction.TurningOn;
            HeaterEvArgs.Status.ActualControllerState = HeaterStatus.ControllerState.ControllerIsOff;
            HeaterEvArgs.Status.ActualOperationState = HeaterStatus.OperationState.Idle;
            _Status = HeaterEvArgs.Status;
            EActivityChanged?.Invoke( this, HeaterEvArgs );
        }

        void InformerExpectingPause()
        {
            HeaterEvArgs.Status.ActualControllerState = HeaterStatus.ControllerState.ControllerIsExpectingPause;
            HeaterEvArgs.Status.ActualOperationState = HeaterStatus.OperationState.RegularOperation;
            _Status = HeaterEvArgs.Status;
            EActivityChanged?.Invoke( this, HeaterEvArgs );
        }
        #endregion

        #region PROTECTED
        virtual protected void ConfirmCommand() { }

        virtual protected void ToggleController()
        {
            _ToggleController = !_ToggleController;
            if (_ToggleController)
            {
                ControllerStart( );
            }
            else
            {
                ControllerStop( );
            }
        }

        protected void Turn( bool value )
        {
            _CommandTurnOn = value;
            _ToggleController = value;
            HeaterEvArgs.TurnOn = _CommandTurnOn;
        }

        protected void ControllerStop()
        {
            Turn( GeneralConstants.OFF );
            HeaterEvArgs.Status.ActualControllerState = HeaterStatus.ControllerState.ControllerIsOff;
            HeaterEvArgs.Status.ActualOperationState = HeaterStatus.OperationState.Idle;
            _Status = HeaterEvArgs.Status;
            EActivityChanged?.Invoke( this, HeaterEvArgs );
        }

        virtual protected void ControllerStart()
        {
            Turn( GeneralConstants.ON );
            HeaterEvArgs.Status.ActualControllerState = HeaterStatus.ControllerState.ControllerIsOn;
            HeaterEvArgs.Status.ActualOperationState = HeaterStatus.OperationState.RegularOperation;
            HeaterEvArgs.Status.ActualActionInfo = HeaterStatus.InformationAction.Finished;
            _Status = HeaterEvArgs.Status;
            EActivityChanged?.Invoke( this, HeaterEvArgs );
        }

        virtual protected void ControllerPause()
        {
            if (( HeaterEvArgs.Status.ActualControllerState == HeaterStatus.ControllerState.ControllerIsOn ) ||
                ( HeaterEvArgs.Status.ActualControllerState == HeaterStatus.ControllerState.ControllerIsExpectingPause ))
            {
                Turn( GeneralConstants.OFF );
                HeaterEvArgs.Status.ActualControllerState = HeaterStatus.ControllerState.ControllerIsPaused;
                HeaterEvArgs.Status.ActualOperationState = HeaterStatus.OperationState.RegularOperation;
                _Status = HeaterEvArgs.Status;
                EActivityChanged?.Invoke( this, HeaterEvArgs );
            }
        }
        #endregion

    }

    public class HeaterControllerPermanent : HeaterController
    {
        public HeaterControllerPermanent( HeaterParameters Parameters, ITimer PauseController, ITimer DelayToggelingController )
            : base( Parameters, PauseController, DelayToggelingController )
        {
        }
    }

    public static class Settings
    {
        static int Factor = 3;
        public static int SignalCountsForPwmLow    =  1 * Factor;
        public static int SignalCountsForPwmMedium = (2 * Factor) - 1;
        public static int SignalCountsForPwmHigh   = (3 * Factor) - 2;
    }

    public class HeaterControllerPulseWidhtModulation : HeaterController
    {
        ControlTimers _HeaterControlTimers;
        HeaterParameters _HeaterParameters;
        bool ToggleSignal = false;
        public new event ActivityChanged EActivityChanged;
        public HeaterControllerPulseWidhtModulation( HeaterParameters Parameters, ControlTimers HeaterControlTimers, ITimer PauseController, ITimer DelayToggelingController )
            : base( Parameters, PauseController, DelayToggelingController )
        {
            _HeaterParameters = Parameters;
            _HeaterControlTimers = HeaterControlTimers;
            _HeaterControlTimers.TimerOnOff.Elapsed += TimerOnOffElapsed;
            _HeaterControlTimers.TimerOnOff.SetTime( Parameters.SignalDurationOn );
            _HeaterControlTimers.TimerLow.Elapsed += ControlTimerLowElapsed;
            _HeaterControlTimers.TimerLow.SetTime( Parameters.SignalDurationLowOn );
            _HeaterControlTimers.TimerSignal.Elapsed += TimerSignalElapsed;
            _HeaterControlTimers.TimerSignal.SetTime( Parameters.SignalDurationSignalisation );
            _HeaterControlTimers.TimerPwm.Elapsed += TimerPwmElapsed;
            _HeaterControlTimers.TimerPwm.SetTime( Parameters.SignalDurationLowOn );
        }
 
        void UpdateStatusAndEventArgs( )
        {
            _Status = HeaterEvArgs.Status;
            EActivityChanged?.Invoke( this, HeaterEvArgs );
        }

        private void TimerOnOffElapsed( object sender, ElapsedEventArgs e )
        {
            HeaterEvArgs.Status.ActualOperationState  = HeaterStatus.OperationState.RegularOperation;
            HeaterEvArgs.Status.ActualControllerState = HeaterStatus.ControllerState.ControllerIsOn;
            HeaterEvArgs.Status.ActualActionInfo      = HeaterStatus.InformationAction.ItensityChanging;
            HeaterEvArgs.Status.ActualPwmState        = HeaterStatus.PwmState.Idle;
            Turn( GeneralConstants.ON );
            _HeaterControlTimers.TimerLow.Start( );
            UpdateStatusAndEventArgs( );
        }

        private void ControlTimerLowElapsed( object sender, ElapsedEventArgs e )
        {
            HeaterEvArgs.Status.ActualOperationState      = HeaterStatus.OperationState.PwmIsWorking;
            HeaterEvArgs.Status.ActualControllerState     = HeaterStatus.ControllerState.ControllerIsOn;
            HeaterEvArgs.Status.ActualActionInfo          = HeaterStatus.InformationAction.ItensityChanging;
            HeaterEvArgs.Status.ActualPwmState            = HeaterStatus.PwmState.Low;
            HeaterEvArgs.Status.ActualSignalisationCounts = Settings.SignalCountsForPwmLow;
            _HeaterControlTimers.TimerPwm.SetTime( _HeaterParameters.SignalDurationLowOn );
            _HeaterControlTimers.TimerPwm.Start( );
            Turn( GeneralConstants.ON );
            _HeaterControlTimers.TimerSignal.Start( );
            UpdateStatusAndEventArgs( );
        }

        private void TimerPwmElapsed( object sender, ElapsedEventArgs e )
        {
            throw new NotImplementedException( );
        }

        private void TimerSignalElapsed( object sender, ElapsedEventArgs e )
        {
            _HeaterControlTimers.TimerSignal.Stop( );
            if (!ToggleSignal)
            {
                Turn( GeneralConstants.OFF );
            }
            else
            {
                Turn( GeneralConstants.ON );
            }
            ToggleSignal = !ToggleSignal;
            if( HeaterEvArgs.Status.ActualSignalisationCounts >= 0 )
            {
                _HeaterControlTimers.TimerSignal.Start( );
                HeaterEvArgs.Status.ActualSignalisationCounts--;
            }
            else // signalisation is finished 
            {

            }
            if( HeaterEvArgs.Status.ActualSignalisationCounts < 0 )
            {
                HeaterEvArgs.Status.ActualSignalisationCounts = 0;
            }
            EActivityChanged?.Invoke( this, HeaterEvArgs );
        }

        override protected void ControllerStart()
        {
            Turn( GeneralConstants.OFF );
            HeaterEvArgs.Status.ActualControllerState = HeaterStatus.ControllerState.ControllerIsOff;
            _HeaterControlTimers.TimerOnOff.Start( );
            HeaterEvArgs.Status.ActualOperationState = HeaterStatus.OperationState.Idle;
            HeaterEvArgs.Status.ActualActionInfo = HeaterStatus.InformationAction.TurningOn;
            _Status = HeaterEvArgs.Status;
            EActivityChanged?.Invoke( this, HeaterEvArgs );
        }

        protected override void ConfirmCommand()
        {
            _HeaterControlTimers.TimerOnOff.Stop( );
            switch( HeaterEvArgs.Status.ActualControllerState )
            {
                case HeaterStatus.ControllerState.ControllerIsOff:
                     HeaterEvArgs.Status.ActualActionInfo = HeaterStatus.InformationAction.Finished;
                     break;

            }
            UpdateStatusAndEventArgs( );
        }
    }

    public class HeaterControllerByTemperature : HeaterController
    {
        public HeaterControllerByTemperature( HeaterParameters Parameters, ITimer PauseController, ITimer DelayToggelingController )
            : base( Parameters, PauseController, DelayToggelingController )
        {

        }
    }
}
