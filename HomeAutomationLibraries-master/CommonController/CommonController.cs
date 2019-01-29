using System;

namespace CommonController
{
    [Serializable]
    public class Controller_State
    {
        public enum ControllerState
        {
            ControllerIsOff,
            ControllerIsOn,
            ControllerIsExpectingPause,
            ControllerIsPaused,
            ControllerInForcedMode,
            ControllerUnforced,
            InvalidForTesting = 99
        }
        public ControllerState ActualControllerState { get; set; }
    }

    public class ControllerEventArgs : EventArgs
    {
        public bool TurnOn { get; set; }
    }

    public delegate void ActivityChanged(object sender, ControllerEventArgs e);

    public class CommonController /*: IController*/
    {

    }
}
