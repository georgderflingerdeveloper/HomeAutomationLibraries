
namespace Signalsequencer
{
    interface IDeviceSequencer
    {
        void Start( );
        void Stop();
        void ReconfigIntervalls( double on, double off );
        void ReconfigIntervalls( double on, double off, double sequenceintervall );
        event SeqUpdate   EUpdate;
    }
}
