using System;

using System.Timers;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LibUdp.BASIC.SEND.INTERFACE;
using LibUdp.BASIC.CONSTANTS;

namespace LibUdp.BASIC.SEND
{
    public class UdpSend : IUdpSend
    {
        IPEndPoint remoteEndPoint;
        UdpClient  client;
        IPAddress  Adress;

        public UdpSend ( string ip_, int port_ )
        {
            if( port_ == 0 )
            {
                throw new Exception( BasicErrorMessage.InvalidPortNumber );
            }

            try 
            {
                Adress = IPAddress.Parse( ip_ );
            }
            catch 
            {
                throw new Exception( BasicErrorMessage.InvalidIpAdress );
            }

            remoteEndPoint = new IPEndPoint( Adress, port_ );
            client         = new UdpClient(  );
        }

        private void sendString ( string message )
        {
            if( String.IsNullOrWhiteSpace( message ) )
            {
                throw( new Exception(message) );
            }
            try
            {
                byte[] data = Encoding.UTF8.GetBytes( message );
                client?.Send( data, data.Length, remoteEndPoint );
            }
            catch
            {

                throw new Exception( BasicErrorMessage.DataSendingFailed );
            }
        }

        public void SendString( string message )
        {
            sendString ( message );
        }

    }
}
