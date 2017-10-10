using HardConfig.COMMON;
using HomeAutomationHeater.INTERFACE;
using System;
using System.Timers;
using TimerMockable;

namespace HomeAutomationHeater
{
    [Serializable]
    public class HeaterStatus
    {
        public enum ControllerState
        {
            ControllerIsOff,
            ControllerIsOn,
            ControllerIsPaused,
            InvalidForTesting = 99
        }

        public ControllerState ActualControllerState { get; set; }

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

        public enum InformationAction
        {
            None,
            TurningOn,
            ItensityChanging,
            TurningOff,
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
        public  float SetTemperatureDay                          { get; set; }
        public  float SetTemperatureNightLowering                { get; set; }
        public  float SetTemperatureEstimationTolerance          { get; set; }
        public  TimeSpan CmdDurationForTurningStartingStopping   { get; set; }
        public  TimeSpan CmdDurationForItensityChange            { get; set; }
        public  TimeSpan CmdDurationForActivatingPause           { get; set; }
        public  TimeSpan SignalDurationLowOn                     { get; set; }
        public  TimeSpan SignalDurationLowOff                    { get; set; }
        public  TimeSpan SignalDurationMiddleOn                  { get; set; }
        public  TimeSpan SignalDurationMiddleOff                 { get; set; }
        public  TimeSpan SignalDurationHighOn                    { get; set; }
        public  TimeSpan SignalDurationHighOff                   { get; set; }
        public  TimeSpan SignalDurationVariableOn                { get; set; }
        public  TimeSpan SignalDurationVariableOff               { get; set; }
        public  TimeSpan SignalDurationBoosting                  { get; set; }
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
        HeaterParameters _Parameters;
        HeaterControllerEventArgs HeaterEvArgs;
        HeaterStatus _Status;
        ITimer _DelayToggelingController;
        #endregion

        #region CONSTRUCTOR
        public HeaterController( HeaterParameters Parameters, ITimer StartStopController, ITimer PauseController, ITimer DelayToggelingController )
        {
            _Parameters = Parameters;
            HeaterEvArgs        = new HeaterControllerEventArgs( );
            HeaterEvArgs.Status = new HeaterStatus( );
            _Status = HeaterEvArgs.Status;
            _DelayToggelingController = DelayToggelingController;
            _DelayToggelingController.Elapsed += DelayToggelingControllerElapsed; 
        }

        #endregion

        #region EVENTHANDLERS
        private void DelayToggelingControllerElapsed( object sender, ElapsedEventArgs e )
        {
            ToggleController( );
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
        }

        public void Pause()
        {
            ControllerPause( );
        }

        public void Resume()
        {
            if( HeaterEvArgs.Status.ActualControllerState == HeaterStatus.ControllerState.ControllerIsPaused )
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

        public void Toggle()
        {
            ToggleController( );
        }

        public void DelayedToggle()
        {
            ToggleController( _Parameters.CmdDurationForTurningStartingStopping );
        }

        public void UpdateParameters( HeaterParameters Parameters )
        {
            _Parameters = Parameters;
        }

        public HeaterStatus GetStatus()
        {
            return _Status;
        }

        public event ActivityChanged EActivityChanged;
        #endregion

        #region PRIVATE
        void ToggleController()
        {
            _ToggleController = !_ToggleController;
            if( _ToggleController )
            {
                ControllerStart( );
            }
            else
            {
                ControllerStop( );
            }
        }        

        void ToggleController( TimeSpan delay )
        {
            _DelayToggelingController.SetTime( delay );
            _DelayToggelingController.Stop( );
            _DelayToggelingController.Start( );
        }  
        
        void Turn( bool value)
        {
            _CommandTurnOn = value;
            _ToggleController = value;
            HeaterEvArgs.TurnOn = _CommandTurnOn;
        }       
        #endregion 

        #region PROTECTED
 
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
            _Status = HeaterEvArgs.Status;
            EActivityChanged?.Invoke( this, HeaterEvArgs );
        }

        virtual protected void ControllerPause()
        {
            if ( HeaterEvArgs.Status.ActualControllerState == HeaterStatus.ControllerState.ControllerIsOn )
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
        public HeaterControllerPermanent( HeaterParameters Parameters, ITimer StartStopController, ITimer PauseController, ITimer DelayToggelingController ) 
            : base( Parameters, StartStopController,  PauseController, DelayToggelingController )
        {
        }
    }

    public class HeaterControllerPulseWidhtModulation : HeaterController
    {
        public HeaterControllerPulseWidhtModulation( HeaterParameters Parameters, ITimer StartStopController, ITimer PauseController, ITimer DelayToggelingController, ITimer TimedItensity ) 
            : base( Parameters, StartStopController,  PauseController, DelayToggelingController )
        {

        }
    }

    public class HeaterControllerByTemperature : HeaterController
    {
        public HeaterControllerByTemperature( HeaterParameters Parameters, ITimer StartStopController, ITimer PauseController, ITimer DelayToggelingController ) 
            : base( Parameters, StartStopController,  PauseController, DelayToggelingController )
        {

        }
    }
}
