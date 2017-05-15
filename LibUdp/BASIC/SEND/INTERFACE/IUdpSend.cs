using System;
namespace LibUdp.BASIC.SEND.INTERFACE
{
    public interface IUdpSend
    {
        void SendString( string message );
    }

    public interface IUdpSendPeriodic
    {
        void SendString( string message, uint counts );
    }
}
