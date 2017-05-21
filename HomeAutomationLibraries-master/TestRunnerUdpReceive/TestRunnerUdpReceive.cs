using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibUdp.BASIC.CONSTANTS;
using LibUdp.BASIC.RECEIVE;

namespace TestRunnerUdpReceive
{
    class TestRunnerUdpReceive
    {
        static void Main(string[] args)
        {
            UdpReceive Receiver = new UdpReceive( BasicIpSettings.DefaultPort );
            Console.WriteLine("Receiving data ...");

            Receiver.EDataReceived += ( sender, e ) => 
            {
                Console.WriteLine( e.Message );
            };

            Console.ReadKey();
        }
    }
}
