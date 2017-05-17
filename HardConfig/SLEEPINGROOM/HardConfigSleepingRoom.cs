using System.Collections.Generic;
using HardConfig.COMMON;
using SystemServices;

namespace HardConfig.SLEEPINGROOM
{
    public static class SleepingRoomIODeviceIndices
    {
        public const int indDigitalInputMainButton = 0;
        public const int indDigitalInputWindowWest = 2;                    // Fenster hinten Richtung Westen
        public const int indDigitalInputMansardWindowNorthLeft = 3;        // Velux Fenster links
        public const int indDigitalInputMansardWindowNorthRight = 4;       // Velux Fenster rechts
        public const int indDigitalInputFireAlert = 5;                     // fire alert - GIRA Rauchmelder

        public const int indDigitalOutputLightMansardRightEnd = 0;         // Leuchte Mansarden ganz rechts
        public const int indDigitalOutputLightBarMansardWindowRight = 1;   // Led Balken Mansarden Fenster rechter Rand 
        public const int indDigitalOutputLightBarMansardWindowMiddle = 2;  // Led Balken zischen linken und rechten Mansarden Fenster 
        public const int indDigitalOutputLightBarMansardWindowLeft = 3;    // Led Balken Mansarden Fenster rechter Rand 
        public const int indDigitalOutputLightCeiling = 4;                 // Leuchte an der Decke
        public const int indDigitalOutputHeater = 5;                       // Heizkörper

        static Dictionary<uint, string> InputDeviceDictionary = new Dictionary<uint, string>
            {
                 { indDigitalInputWindowWest,                        SleepingRoomDeviceNames.WindowWest                  },
                 { indDigitalInputMansardWindowNorthLeft,            SleepingRoomDeviceNames.MansardWindowNorthLeft      },
                 { indDigitalInputMansardWindowNorthRight,           SleepingRoomDeviceNames.MansardWindowNorthRight     },
                 { indDigitalInputFireAlert,                         SleepingRoomDeviceNames.FireAlert                   },
            };

        static Dictionary<uint, string> OutputDeviceDictionary = new Dictionary<uint, string>
            {
                 { indDigitalOutputHeater,                           SleepingRoomDeviceNames.Heater                      },
                 { indDigitalOutputLightMansardRightEnd,             SleepingRoomDeviceNames.LightMansardRightEnd        },
                 { indDigitalOutputLightBarMansardWindowRight,       SleepingRoomDeviceNames.LightBarMansardWindowRight  },
                 { indDigitalOutputLightBarMansardWindowMiddle,      SleepingRoomDeviceNames.LightBarMansardWindowMiddle },
                 { indDigitalOutputLightBarMansardWindowLeft,        SleepingRoomDeviceNames.LightBarMansardWindowLeft   },
                 { indDigitalOutputLightCeiling,                     SleepingRoomDeviceNames.LightCeiling                },
            };

        public static string GetInputDeviceName( uint key )
        {
            return ( GetData.ValueFromDeviceDictionary( InputDeviceDictionary, key ) );
        }

        public static string GetOutputDeviceName( uint key )
        {
            return ( GetData.ValueFromDeviceDictionary( OutputDeviceDictionary, key ) );
        }
    }

    static class SleepingRoomDeviceNames
    {
        public const string Prefix = InfoOperationMode.SLEEPING_ROOM + Seperators.InfoSeperator;
        public const string WindowWest = Prefix + "WindowWest";
        public const string MansardWindowNorthLeft = Prefix + "MansardWindowNorthLeft";
        public const string MansardWindowNorthRight = Prefix + "MansardWindowNorthRight";
        public const string FireAlert = Prefix + "FireAlert";
        public const string Heater = Prefix + "Heater";
        public const string LightMansardRightEnd = Prefix + "LightMansardRightEnd";
        public const string LightBarMansardWindowRight = Prefix + "LightBarMansardWindowRight";
        public const string LightBarMansardWindowMiddle = Prefix + "LightBarMansardWindowMiddle";
        public const string LightBarMansardWindowLeft = Prefix + "LightBarMansardWindowLeft";
        public const string LightCeiling = Prefix + "LightCeiling";
    }

    static class SleepingIOLightIndices
    {
        public const int indSleepingRoomFirstLight = 0;
        public const int indSleepingRoomSecondLight = 1;
        public const int indSleepingRoomThirdLight = 2;
        public const int indSleepingRoomFourthLight = 3;
        public const int indSleepingRoomLastLight = 4;
    }

    static class ParametersHeaterControlSleepingRoom
    {
        static readonly public double TimeDemandForHeatersAutomaticOff = TimeConverter.ToMiliseconds( 10, 0, 0 );
    }

}
