using System;
using LibUdp.BASIC.SEND;
using TimerMockable;

namespace LibraryTestRunner
{
    class MainClass
    {
        public static void Main( string[] args )
        {
            UdpSendPeriodic TestSendPeriodic = new UdpSendPeriodic( "127.0.0.1", 5000, new Timer_(500) );
            Console.WriteLine( "Send UDP packets:" );
            TestSendPeriodic.EDataSendingStatus += (sender, e) =>
            {
                Console.WriteLine("Status: " + e.Status.ToString());
                Console.WriteLine("Count:" + e.ActualCounts.ToString());
            };
           TestSendPeriodic.SendString( "Hello", 10);
           Console.ReadKey();
        }
    }
}
