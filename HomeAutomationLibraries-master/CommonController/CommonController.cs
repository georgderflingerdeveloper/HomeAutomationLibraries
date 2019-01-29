using System;

namespace UnivCommonController
{
    [Serializable]
    public class ControllerConstants
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
        protected ControllerConstants State;

        public CommonController()
        {
            State = new ControllerConstants();
        }

        public void Start()
        {
            if (IsForced())
            {
                return;
            }
            ControllerStart();
        }

        public void Stop()
        {
            if (IsForced())
            {
                return;
            }
            ControllerStop();
        }

        protected bool IsForced()
        {
            return (
                State.ActualControllerState
                ==
                ControllerConstants.ControllerState.ControllerInForcedMode ? true : false);
        }

        protected bool IsControllerOn()
        {
            return (
                State.ActualControllerState
                ==
                ControllerConstants.ControllerState.ControllerIsOn ? true : false);
        }

        virtual protected void Turn(bool value)
        {
        }

        virtual protected void ControllerStart()
        {
        }

        virtual protected void ControllerStop()
        {
        }
    }
}
