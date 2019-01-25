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

            HeaterControllerThermostate Controller = new HeaterControllerThermostate( new HeaterParameters(), ControlTimers_ );
            Controller.EActivityChanged += (sender, e) => 
            {
                Console.WriteLine( e.Status.ActualControllerState );
                
            };

            string InputKey;

            do{
                Console.WriteLine( "Type (s)start / (t)stop controller" );
                InputKey = Console.ReadLine();
                switch( InputKey )
                {
                    case "s":
                        Controller.Start();
                        break;
                    case "t":
                        Controller.Stop();
                        break;

                }
            } while( InputKey != "q" );

            Console.ReadLine();
        }
    }
}
