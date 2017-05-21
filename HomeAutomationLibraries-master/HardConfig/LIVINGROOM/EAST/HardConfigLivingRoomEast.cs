using System;
using System.Collections.Generic;
using HardConfig.COMMON;
using SystemServices;

namespace HardConfig.LIVINGROOM.EAST
{
    static class LivingRoomEastHardwareAssignment
    {
        public const int indInterfaceCard_1 = 0;
        public const int indInterfaceCard_2 = 1;
    }

    static class LivingRoomEastIOAssignment
    {
        public static class LivDigInputs
        {
        }
    }

    static class ParametersLightControlEASTSide
    {
        static readonly public double TimeDemandForAutomaticOffEastSide = TimeConverter.ToMiliseconds( 1, 0, 0 );
        static readonly public double TimeDemandForAllOn = TimeConverter.ToMiliseconds( 2.5 );
    }

    static class EastDeviceNames
    {
        public const string Prefix = InfoOperationMode.LIVING_ROOM_EAST + Seperators.WhiteSpace;
        public const string TestButton = Prefix + "Test Knopf";
        public const string DoorSwitch = Prefix + "Tür Schalter Eingang Haupt (rechts)";
    }

    static class EastSideIOAssignment
    {
        public const int indTestButton                       = 0;
        public const int indSpotFrontSide1_4                 = 0;
        public const int indSpotFrontSide5_8                 = 1;
        public const int indSpotBackSide1_3                  = 2;
        public const int indSpotBackSide4_8                  = 3;
        public const int indLightsTriangleGalleryBack        = 4;
        public const int indDoorEntry_Window_Right           = 5;
        public const int indWindowBesideDoorRight            = 6;
        public const int indWindowLEDEastUpside              = 7;
        public const int indWindowLimitSwitchWestUpside      = 2;
        public const int indSpotGalleryFloor_1_18            = 0;
        public const int indSpotGalleryFloor_2_4             = 1;
        public const int indSpotGalleryFloor_5_6             = 3;
        public const int indSpotGalleryFloor_7               = 4;
        public const int indSpotGalleryFloor_8_10            = 5;
        public const int indSpotGalleryFloor_11_12           = 6;
        public const int indSpotGalleryFloor_13              = 7;
        public const int indSpotGalleryFloor_14_15           = 8;
        public const int indSpotGalleryFloor_16              = 9;
        public const int indSpotGalleryFloor_17_19_20_21     = 10;
        public const int indBarGallery1_4                    = 11;
        public const int indDigitalInput_PresenceDetector    = 5;
        public const int indDigitalInput_MainDoorWingRight   = 4;
        public const int indDigitalInput_DoorSwitchMainRight = 7;

        public static uint SerialCard1 { get; set; }

        static Dictionary<uint, string> EastInputDeviceDictionary = new Dictionary<uint, string>
            {
                { indTestButton                       + SerialCard1,             EastDeviceNames.TestButton         },
                { indDigitalInput_DoorSwitchMainRight + SerialCard1,             EastDeviceNames.DoorSwitch        },
            };

        public static string GetInputDeviceName( int key )
        {
            return ( GetData.ValueFromDeviceDictionary( EastInputDeviceDictionary, Convert.ToUInt32( key ) ) );
        }

    }
}
