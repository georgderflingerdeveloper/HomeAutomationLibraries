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
        public ControllerInformer Informer { get; set; }
    }

    public delegate void ActivityChanged(object sender, ControllerEventArgs e);

    public class CommonController : IController
    {
        #region DECLARATION
        protected ControllerInformer Informer;
        protected ControllerEventArgs CommonEventArgs;
        #endregion

        #region CONSTRUCTOR
        public CommonController()
        {
            Informer = new ControllerInformer();
            CommonEventArgs = new ControllerEventArgs
            {
                Informer = Informer
            };
        }
        #endregion

        #region INTERFACE_IMPLEMENTATION
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
            Force_();
        }

        public void UnForce()
        {
            Unforce_();
        }

        public void ForcedOff()
        {
            if (IsForced())
            {
                Turn(GeneralConstants.OFF);
                Update();
            }
        }

        public void Reset()
        {
        }

        public event ActivityChanged EActivityChanged;
        #endregion

        #region PROTECTED
        protected bool IsForced()
        {
            return (
                Informer.ActualControllerState
                ==
                ControllerInformer.ControllerState.ControllerInForcedMode ? true : false);
        }

        protected bool IsControllerOn()
        {
            return (
                Informer.ActualControllerState
                ==
                ControllerInformer.ControllerState.ControllerIsOn ? true : false);
        }

        protected void SetControllerState(ControllerInformer.ControllerState state)
        {
            CommonEventArgs.Informer.ActualControllerState = state;
        }

        #endregion

        #region PRIVATE
        #endregion

        #region VIRTUAL
        virtual protected void Turn(bool value)
        {
            CommonEventArgs.TurnOn = value;
        }

        virtual protected void ControllerStart()
        {
            Turn(GeneralConstants.ON);
            SetControllerState(ControllerInformer.ControllerState.ControllerIsOn);
            Update();
        }

        virtual protected void ControllerStop()
        {
            Turn(GeneralConstants.OFF);
            SetControllerState(ControllerInformer.ControllerState.ControllerIsOff);
            Update();
        }

        virtual protected void Update()
        {
            EActivityChanged?.Invoke(this, CommonEventArgs);
        }

        virtual protected void Force_()
        {
        }

        virtual protected void Unforce_()
        {
        }
        #endregion
    }
}
