using System;
using LibUdp.BASIC.SEND;
using TimerMockable;

namespace TestRunnerUdpSendPeriodicStartStop
{
    class MainClass
    {

        public static void Main( string[] args )
        {
            Console.WriteLine( "UDP Sender - press (s) for start or (e) for end" );

            UdpSendPeriodic SenderPeriodic = new UdpSendPeriodic( "127.0.0.1", 5000, new Timer_(500) );

            SenderPeriodic.EDataSendingStatus += (sender, e) => 
            {
                Console.WriteLine( "Sendpacket with number: " + SenderPeriodic.ActualCounts.ToString() + " Status = " + e.Status.ToString() );
            };

            string command;
            do
            {
                command = Console.ReadLine();
                switch( command )
                {
                    case "s":
                         SenderPeriodic.SendStringCyclic( "Hello" );
                         break;

                    case "e":
                         SenderPeriodic.StopSendStringCyclic();
                         break;
                }
            }while ( command != "e" );
        }
    }
}
