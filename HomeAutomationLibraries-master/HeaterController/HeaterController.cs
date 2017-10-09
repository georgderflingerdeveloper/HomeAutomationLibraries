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
        public float SetTemperatureDay                         { get; set; }
        public float SetTemperatureNightLowering               { get; set; }
        public float SetTemperatureEstimationTolerance         { get; set; }
        public TimeSpan ControlSignalDurationForTurningOn      { get; set; }
        public TimeSpan ControlSignalDurationForTurningOff     { get; set; }
        public TimeSpan ControlSignalDurationForItensityChange { get; set; }

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
        HeaterControllerEventArgs HeaterEvArgs;
        #endregion

        #region CONSTRUCTOR
        public HeaterController( ITimer StartController, ITimer StopController )
        {
            HeaterEvArgs = new HeaterControllerEventArgs( );
            HeaterEvArgs.Status = new HeaterStatus( );
        }
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
            ControllerToggleControlSignal( );
        }

        public void Toggle( TimeSpan delay )
        {

        }
        public event ActivityChanged EActivityChanged;
        #endregion

        #region PROTECTED
        void Turn( bool value)
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

        void ControllerToggleControlSignal()
        {
            _ToggleController = !_CommandTurnOn;
            if( _ToggleController )
            {
                ControllerStart( );
            }
            else
            {
                ControllerStop( );
            }
        }
        #endregion

    }

    public class HeaterControllerPermanent : HeaterController
    {
        public HeaterControllerPermanent( ITimer StartController, ITimer StopController ) : base(  StartController,  StopController )
        {

        }
    }

    public class HeaterControllerPulseWidhtModulation : HeaterController
    {
        public HeaterControllerPulseWidhtModulation( ITimer StartController, ITimer StopController, ITimer TimedItensity ) : base(  StartController,  StopController )
        {

        }
    }

    public class HeaterControllerByTemperature : HeaterController
    {
        public HeaterControllerByTemperature( ITimer StartController, ITimer StopController ) : base(  StartController,  StopController )
        {

        }
    }
}
