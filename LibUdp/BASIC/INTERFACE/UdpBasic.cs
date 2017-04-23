using System;
using System.Timers;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LibUdp
{
    static class UDPConfig
    {
        public static void GetIPAdr ( string hostname )
        {
            IPAddress[] addresslist = Dns.GetHostAddresses( hostname );
        }
        public static int    port              =  7000;
        public static string IpAdressBroadcast =  "192.168.0.255";
        public static string IpAdress          =  "127.0.0.1";       //"192.168.0.105";
    }

    class UdpSend
    {
        private string IP = UDPConfig.IpAdress;  
        public int port   = UDPConfig.port;  

        IPEndPoint remoteEndPoint;
        UdpClient  client;

        public UdpSend ( )
        {
            remoteEndPoint = new IPEndPoint( IPAddress.Parse( IP ), port );
            client         = new UdpClient( );
        }

        public UdpSend ( string ip_, int port_ )
        {
            remoteEndPoint = new IPEndPoint( IPAddress.Parse( ip_ ), port_ );
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
   
                throw new Exception("Data sending failed");
            }
        }

        public void SendString( string message )
        {
            sendString ( message );
        }

        string _SendText;
        public string SendText
        {
            set
            {
                _SendText = value;
                sendString( value );
            }
        }
    }

    public class UdpBasic
    {
        public UdpBasic( )
        {
        }
    }
}
