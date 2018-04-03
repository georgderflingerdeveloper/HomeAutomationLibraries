using System;
using System.Timers;
using TimerMockable;
using HomeAutomationHeater;

namespace TestRunnerHeaterController
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ControlTimers ControlTimers_ = new InialisedTimers( ).Timers;

            HeaterControllerPulseWidhtModulation Controller = new HeaterControllerPulseWidhtModulation( new HeaterParameters(), ControlTimers_ );
            Controller.EActivityChanged += (sender, e) => 
            {
                Console.WriteLine( "Controller status = " + e.Status.ActualControllerState );
                
            };

            string InputKey;

            do{
                Console.WriteLine( "Type (s) for start controller" );
                InputKey = Console.ReadLine();
                switch( InputKey )
                {
                    case "s":
                        Controller.Start();
                        break;
                }
            }while( InputKey != "q" );

            Console.ReadLine();
        }
    }
}
