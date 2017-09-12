using System;
using System.Timers;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LibUdp.BASIC.SEND.INTERFACE;
using LibUdp.BASIC.CONSTANTS;
using TimerMockable;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;


namespace LibUdp.BASIC.SEND
{
    public class UdpSend : IUdpSend
    {
        IPEndPoint remoteEndPoint;
        UdpClient client;
        IPAddress Adress;

        public UdpSend( string ip_, int port_ )
        {
            Adress = IPAddress.Parse( ip_ );
            remoteEndPoint = new IPEndPoint( Adress, port_ );
            client = new UdpClient( );
        }

        
        protected void AsynCsendMessage( string message )
        {
            if( String.IsNullOrWhiteSpace( message ) )
            {
                throw (new Exception( BasicErrorMessage.InvalidSendString ));
            }
            byte[] data = Encoding.UTF8.GetBytes( message );
            client?.SendAsync( data, data.Length, remoteEndPoint );
        }

        protected void sendMessage( string message )
        {
            if ( String.IsNullOrWhiteSpace( message ) )
            {
                throw ( new Exception( BasicErrorMessage.InvalidSendString ) );
            }
            byte[] data = Encoding.UTF8.GetBytes( message );
            client?.Send( data, data.Length, remoteEndPoint );
        }

        public void SendString( string message )
        {
            AsynCsendMessage( message );
        }

        public void SyncSendString( string message )
        {
            sendMessage( message );
        }
    }
}
