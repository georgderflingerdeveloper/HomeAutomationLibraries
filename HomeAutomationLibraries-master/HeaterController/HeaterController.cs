using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeaterController
{
    [Serializable]
    public class HeaterStatus
    {
        public enum OperationMode
        {
            Permanent,
            PulseWidthModulation,
            TemperatureControlHystersis
        }

        public OperationMode SelectedOperationMode { get; set; }

        public enum ControllerState
        {
            ControllerIsOff,
            ControllerIsOn,
            ControllerIsPaused
        }

        public ControllerState ActualControllerState { get; }

        public enum OperationState
        {
            RegularOperation,
            Boosting,
            Defrosting
        }

        public OperationState ActualOperationState { get; }

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
        public float SetTemperatureDay           { get; set; }
        public float SetTemperatureNightLowering { get; set; }

    }

    public class HeaterControllerEventArgs : EventArgs
    {
        public bool TurnOn { get; set; }
    }

    public delegate void ActivityChanged( object sender, HeaterControllerEventArgs e );

    public class HeaterController
    {
    }
}
