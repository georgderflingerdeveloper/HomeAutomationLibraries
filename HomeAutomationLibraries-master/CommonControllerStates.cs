using System;

namespace Controllers
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

}
