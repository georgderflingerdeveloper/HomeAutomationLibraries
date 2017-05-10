using System;
namespace LibUdp.BASIC.SEND
{
    public interface IUdpSendPeriodic
    {
        void SendMessageWithHeader( string message, uint counts );
        void SendPeriodicMessage( string message );
        void SendPeriodicMessageWithHeader( string message );
        void StopSendPeriodicMessage();
    }
}
