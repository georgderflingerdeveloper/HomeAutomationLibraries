using System;
namespace LibUdp.BASIC.SEND.INTERFACE
{
    public interface IUdpSend
    {
        void SendString( string message );
    }

    public interface IUdpSendPeriodic
    {
        UdpSendPeriodic SendString( string message, uint counts );
    }
}
