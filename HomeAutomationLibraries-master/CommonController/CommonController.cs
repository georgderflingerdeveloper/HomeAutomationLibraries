using System;
using HardConfig.COMMON;

namespace UnivCommonController
{
    [Serializable]
    public class ControllerInformer
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
        protected ControllerInformer State;

        public CommonController()
        {
            State = new ControllerInformer();
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

        public void ForcedOn()
        {
            if (IsForced())
            {
                Turn(GeneralConstants.ON);
                Update();
            }
        }

        public void Force()
        {
            force_();
        }

        public void UnForce()
        {
            unforce_();
        }

        public void ForcedOff()
        {
            if (IsForced())
            {
                Turn(GeneralConstants.OFF);
                Update();
            }
        }

        protected bool IsForced()
        {
            return (
                State.ActualControllerState
                ==
                ControllerInformer.ControllerState.ControllerInForcedMode ? true : false);
        }

        protected bool IsControllerOn()
        {
            return (
                State.ActualControllerState
                ==
                ControllerInformer.ControllerState.ControllerIsOn ? true : false);
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

        virtual protected void  Update()
        {
        }

        virtual protected void force_()
        {
        }

        virtual protected void unforce_()
        {
        }
    }
}
