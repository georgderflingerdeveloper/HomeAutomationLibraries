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

 
    public class UdpBasic
    {
        public UdpBasic( )
        {
        }
    }
}
