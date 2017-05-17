using System;
using SystemServices;

namespace HardConfig.ANTEROOM
{
    static class AnteRoomIOAssignment
    {
        public const int indAnteRoomMainButton = 0;
        public const int indWashRoomMainButton = 1;
        public const int indBathRoomMainButton = 2;
        public const int indAnteRoomPresenceDetector = 3;
    }

    static class ParametersLightControlAnteRoom
    {
        static readonly public double TimeDemandAllOutputsOff = TimeConverter.ToMiliseconds( 10 );
        static readonly public double TimeDemandForAutomaticOffAnteRoom = TimeConverter.ToMiliseconds( 5 );
        static readonly public double TimeDemandForAllOn = TimeConverter.ToMiliseconds( 3.5 );
        static readonly public double TimeDemandAutomaticOffViaPresenceDetector = TimeConverter.ToMiliseconds( 1, 0 );
        static readonly public double TimeDemandForAllOffFinal = TimeConverter.ToMiliseconds( 3, 0, 0 );
    }

    static class AnteRoomIODeviceIndices
    {
        public const int indDigitalOutputAnteRoomHeater = 12;
    }

    static class AnteRoomIOLightIndices
    {
        public const int indFirstLight = 0;
        public const int indLastLight = 4;
        public const int indFirstWashroom = 5;
        public const int indLastWashroom = 6;
    }

    static class AnteRoomIOLightIndexNaming
    {
        public const int AnteRoomMainLight = 0;
        public const int AnteRoomBackSide = 1;
        public const int AnteRoomRoofBackSideFloorSpotGroupMiddle1 = 2;
        public const int AnteRoomRoofBackSideFloorSpotGroupMiddle2 = 3;
        public const int AnteRoomNightLight = 4;
    }

}
