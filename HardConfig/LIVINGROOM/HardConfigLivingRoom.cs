using System;
using System.Collections.Generic;
using HardConfig.COMMON;


namespace HardConfig.LIVINGROOM
{
    class HardConfigLivingRoom
    {
    }

    static class KitchenIOAssignment
    {
        public const int indKitchenMainButton = 0;
        public const int indKitchenPresenceDetector = 7;
    }

    static class CenterLivingRoomIODeviceIndices
    {
        public const int indDigitalOutputBoiler = 10;
        public const int indDigitalOutputPumpHeatingSystem = 8;
        public const int indDigitalInputDoorEntryAnteRoom = 8;
        public const int indDigitalInputPowerMeter = 9;
    }

    static class CenterOutsideDeviceNames
    {
        public const string Prefix = InfoOperationMode.CENTER_KITCHEN_AND_LIVING_ROOM + Seperators.InfoSeperator;
        public const string Rainsensor = Prefix + "Rainsensor";
    }

    static class CenterOutsideIODevices
    {
        public const int indDigitalOutputLightsOutside = 15;
        public const int indDigitalInputSecondaryLightControlOutside = 9;
        public const int indDigitalInputRainSensor = 10;

        static Dictionary<uint, string> InputDeviceDictionary = new Dictionary<uint, string>
            {
                 { indDigitalInputRainSensor,                 CenterOutsideDeviceNames.Rainsensor                 },
            };

        static Dictionary<uint, string> OutputDeviceDictionary = new Dictionary<uint, string>
        {
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

    static class CenterKitchenDeviceNames
    {
        public const string Prefix = InfoOperationMode.CENTER_KITCHEN_AND_LIVING_ROOM + Seperators.WhiteSpace;
        public const string LightSightCabinet = Prefix + "LED Balken Seitenschrank mit Elektroverteilung";
        public const string MainButton = Prefix + "Haupt Taster";
        public const string PresenceDetector = Prefix + "Bewegungsmelder neben E-Vereiler";
        public const string ButtonSleepingRoom = Prefix + "Taster Schlafzimmer";
        public const string ButtonAnteRoom = Prefix + "Taster Vorhaus";
        public const string ButtonBathRoom = Prefix + "Taster Badezimmer";
        public const string ButtonWashRoom = Prefix + "Taster WC";
        public const string FrontLight1 = Prefix + "Lichtbalken Küche 1 ( ganz rechts und links )";
        public const string FrontLight2 = Prefix + "Lichtbalken Küche 2 ( von rechts )";
        public const string FrontLight3 = Prefix + "Lichtbalken Küche 3 ( von rechts )";
        public const string FumeHood = Prefix + "Lichtbalken am Dunstabzug";
        public const string Slot = Prefix + "Lichtbalken im Zwischenraum";
        public const string KitchenCabinet = Prefix + "Lichtbalken über Küchenschrank";
        public const string WindowBoardEastDown = Prefix + "Zierlicht Fensterbalken Ost";
        public const string Boiler = Prefix + "Warmwasser Boiler";
        public const string CirculationPump = Prefix + "Zirkulationspumpe für Warmwasser";
        public const string HeaterEast = Prefix + "Thermostatkopf Heizung Ost";
        public const string HeaterWest = Prefix + "Thermostatkopf Heizung West";
        public const string FanWashRoom = Prefix + "Lüfter WC";
    }

    static class CenterButtonRelayIOAssignment
    {
        public const int indDigitalInputRelaySleepingRoom = 3;
        public const int indDigitalInputRelayWashRoom = 4;
        public const int indDigitalInputRelayAnteRoom = 5;
        public const int indDigitalInputRelayBathRoom = 6;
    }

    static class WaterHeatingSystemIODeviceIndices
    {
        public const int indDigitalOutputWarmWaterCirculationPump = 11;
    }

    static class KitchenLivingRoomIOAssignment
    {
        public const int indFirstHeater = 13;
        public const int indLastHeater = 14;
    }

    static class WashRoomIODeviceIndices
    {
        public const int indDigitalOutputWashRoomLight = 5;
        public const int indDigitalOutputWashRoomFan = 12;
    }

    static class KitchenCenterIoDevices
    {
        public const int indDigitalOutputFirstKitchen = 0;
        public const int indDigitalOutputFrontLight_1 = 1;
        public const int indDigitalOutputFrontLight_2 = 2;
        public const int indDigitalOutputFrontLight_3 = 3;
        public const int indDigitalOutputFumeHood = 4;
        public const int indDigitalOutputSlot = 5;
        public const int indDigitalOutputKitchenKabinet = 6;
        public const int indDigitalOutputWindowBoardEastDown = 9;
        static readonly public int indLastKitchen = indDigitalOutputKitchenKabinet;

        static Dictionary<uint, string> CenterInputDeviceDictionary = new Dictionary<uint, string>
            {
                { KitchenIOAssignment.indKitchenMainButton,                                   CenterKitchenDeviceNames.MainButton         },
                { KitchenIOAssignment.indKitchenPresenceDetector,                             CenterKitchenDeviceNames.PresenceDetector   },
                { CenterButtonRelayIOAssignment.indDigitalInputRelayAnteRoom,                 CenterKitchenDeviceNames.ButtonAnteRoom     },
                { CenterButtonRelayIOAssignment.indDigitalInputRelayBathRoom,                 CenterKitchenDeviceNames.ButtonBathRoom     },
                { CenterButtonRelayIOAssignment.indDigitalInputRelaySleepingRoom,             CenterKitchenDeviceNames.ButtonSleepingRoom },
                { CenterButtonRelayIOAssignment.indDigitalInputRelayWashRoom,                 CenterKitchenDeviceNames.ButtonWashRoom     },
            };

        static Dictionary<uint, string> CenterOutputDeviceDictionary = new Dictionary<uint, string>
            {
                { indDigitalOutputFirstKitchen,                                               CenterKitchenDeviceNames.LightSightCabinet  },
                { indDigitalOutputFrontLight_1,                                               CenterKitchenDeviceNames.FrontLight1        },
                { indDigitalOutputFrontLight_2,                                               CenterKitchenDeviceNames.FrontLight2        },
                { indDigitalOutputFrontLight_3,                                               CenterKitchenDeviceNames.FrontLight3        },
                { indDigitalOutputFumeHood,                                                   CenterKitchenDeviceNames.FumeHood           },
                { indDigitalOutputSlot,                                                       CenterKitchenDeviceNames.Slot               },
                { indDigitalOutputKitchenKabinet,                                             CenterKitchenDeviceNames.KitchenCabinet     },
                { indDigitalOutputWindowBoardEastDown,                                        CenterKitchenDeviceNames.KitchenCabinet     },
                { CenterLivingRoomIODeviceIndices.indDigitalOutputBoiler,                     CenterKitchenDeviceNames.Boiler             },
                { WaterHeatingSystemIODeviceIndices.indDigitalOutputWarmWaterCirculationPump, CenterKitchenDeviceNames.CirculationPump    },
                { KitchenLivingRoomIOAssignment.indFirstHeater,                               CenterKitchenDeviceNames.HeaterEast         },
                { KitchenLivingRoomIOAssignment.indLastHeater,                                CenterKitchenDeviceNames.HeaterWest         },
                { WashRoomIODeviceIndices.indDigitalOutputWashRoomFan,                        CenterKitchenDeviceNames.FanWashRoom        }
            };

        public static string GetInputDeviceName( int key )
        {
            return ( GetData.ValueFromDeviceDictionary( CenterInputDeviceDictionary, Convert.ToUInt32( key ) ) );
        }
        public static string GetOutputDeviceName( int key )
        {
            return ( GetData.ValueFromDeviceDictionary( CenterOutputDeviceDictionary, Convert.ToUInt32( key ) ) );
        }
    }

}
