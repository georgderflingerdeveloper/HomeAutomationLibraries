using HomeAutomationHeater;
namespace HomeAutomationHeater.INTERFACE
{
    interface IHeaterControl
    {
        void Confirm();
        void DelayedPause();
        void Pause();
        void Resume();
        void Toggle();
        void DelayedToggle();
        void UpdateParameters( HeaterParameters Parameters );
        HeaterStatus GetStatus();
        event ActivityChanged EActivityChanged;
    }
}
