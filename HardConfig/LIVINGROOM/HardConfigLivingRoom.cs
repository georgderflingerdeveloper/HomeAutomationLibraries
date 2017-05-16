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

}
