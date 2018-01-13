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
            HeaterControllerPulseWidhtModulation Controller = new HeaterControllerPulseWidhtModulation( new HeaterParameters(), new ControlTimers() , new Timer_(500), new Timer_(500) );
            Controller.EActivityChanged += (sender, e) => 
            {
                Console.WriteLine( "Controller status = " + e.Status.ActualControllerState );
                
            };

            string InputKey;

            do{
                Console.WriteLine( "Type (s) for start controller" );
                InputKey = Console.ReadKey().ToString();
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
