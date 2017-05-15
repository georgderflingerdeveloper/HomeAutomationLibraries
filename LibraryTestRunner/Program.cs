using System;
using LibUdp.BASIC.SEND;
using TimerMockable;
using SystemServices;

namespace LibraryTestRunner
{
    class MainClass
    {
        public static void Main( string[] args )
        {
            UdpSendPeriodic TestSendPeriodic = new UdpSendPeriodic( "127.0.0.1", 5000, new Timer_(500), new TimeUtil() );
            Console.WriteLine( "Send UDP packets:" );
            TestSendPeriodic.EDataSendingStatus += (sender, e) =>
            {
                Console.WriteLine("Status: " + e.Status.ToString() );
                Console.WriteLine("Count:"   + e.ActualCounts.ToString() );
            };
            TestSendPeriodic.SendMessage( "Hello", 10);
            Console.ReadKey();
        }
    }
}
