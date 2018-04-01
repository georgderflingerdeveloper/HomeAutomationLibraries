using LibUdp.BASIC.RECEIVE;

namespace LibUdp.BASIC.INTERFACE
 {
    public interface IUdpBasic
    {
        event DataReceived EDataReceived;
        void SendString( string message );
    }
}
