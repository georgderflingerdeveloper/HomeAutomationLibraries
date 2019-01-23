namespace HomeAutomationHeater.INTERFACE
{
    interface IHeaterControl
    {
        void Start();
        void Confirm();
        void DelayedPause();
        void Pause();
        void Resume();
        void Stop();
        void Toggle();
        void DelayedToggle();
        void Reset();
        void Force();
        void ForcedOn();
        void ForcedOff();
        void UpdateParameters( HeaterParameters Parameters );
        HeaterStatus GetStatus();
        event ActivityChanged EActivityChanged;
    }
}
