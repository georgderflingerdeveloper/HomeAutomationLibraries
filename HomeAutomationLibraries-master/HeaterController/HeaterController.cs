using System;
using System.Timers;
using TimerMockable;
using HomeAutomationHeater.INTERFACE;
using HardConfig.COMMON;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public  float SetTemperatureDay                         { get; set; }
        public  float SetTemperatureNightLowering               { get; set; }
        public  float SetTemperatureEstimationTolerance         { get; set; }
        public  TimeSpan ControlSignalDurationForTurningOnOff   { get; set; }
        public  TimeSpan ControlSignalDurationForItensityChange { get; set; }
        public  TimeSpan SignalDurationLowOn                    { get; set; }
        public  TimeSpan SignalDurationLowOff                   { get; set; }
        public  TimeSpan SignalDurationMiddleOn                 { get; set; }
        public  TimeSpan SignalDurationMiddleOff                { get; set; }
        public  TimeSpan SignalDurationHighOn                   { get; set; }
        public  TimeSpan SignalDurationHighOff                  { get; set; }
        public  TimeSpan SignalDurationVariableOn               { get; set; }
        public  TimeSpan SignalDurationVariableOff              { get; set; }
        public  TimeSpan SignalDurationBoosting                 { get; set; }
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
        ITimer _DelayToggelingController;
        #endregion

        #region CONSTRUCTOR
        public HeaterController( HeaterParameters Parameters, ITimer StartController, ITimer StopController, ITimer DelayToggelingController )
        {
            _Parameters = Parameters;
            HeaterEvArgs = new HeaterControllerEventArgs( );
            HeaterEvArgs.Status = new HeaterStatus( );
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

        public void Pause( TimeSpan delay )
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
            ToggleController( _Parameters.ControlSignalDurationForTurningOnOff );
        }

        public void UpdateParameters( HeaterParameters Parameters )
        {
            _Parameters = Parameters;
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
            EActivityChanged?.Invoke( this, HeaterEvArgs );
        }

        virtual protected void ControllerStart()
        {
            Turn( GeneralConstants.ON );
            HeaterEvArgs.Status.ActualControllerState = HeaterStatus.ControllerState.ControllerIsOn;
            HeaterEvArgs.Status.ActualOperationState = HeaterStatus.OperationState.RegularOperation;
            EActivityChanged?.Invoke( this, HeaterEvArgs );
        }

        virtual protected void ControllerPause()
        {
            if ( HeaterEvArgs.Status.ActualControllerState == HeaterStatus.ControllerState.ControllerIsOn )
            {
                Turn( GeneralConstants.OFF );
                HeaterEvArgs.Status.ActualControllerState = HeaterStatus.ControllerState.ControllerIsPaused;
                HeaterEvArgs.Status.ActualOperationState = HeaterStatus.OperationState.RegularOperation;
                EActivityChanged?.Invoke( this, HeaterEvArgs );
            }
        }

 
        #endregion

    }

    public class HeaterControllerPermanent : HeaterController
    {
        public HeaterControllerPermanent( HeaterParameters Parameters, ITimer StartController, ITimer StopController, ITimer DelayToggelingController ) 
            : base( Parameters, StartController,  StopController, DelayToggelingController )
        {

        }
    }

    public class HeaterControllerPulseWidhtModulation : HeaterController
    {
        public HeaterControllerPulseWidhtModulation( HeaterParameters Parameters, ITimer StartController, ITimer StopController, ITimer DelayToggelingController, ITimer TimedItensity ) 
            : base( Parameters, StartController,  StopController, DelayToggelingController )
        {

        }
    }

    public class HeaterControllerByTemperature : HeaterController
    {
        public HeaterControllerByTemperature( HeaterParameters Parameters, ITimer StartController, ITimer StopController, ITimer DelayToggelingController ) 
            : base( Parameters, StartController,  StopController, DelayToggelingController )
        {

        }
    }
}
