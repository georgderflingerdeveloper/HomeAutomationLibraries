using System;
using System.Collections.Generic;
using HardConfig.COMMON;
using SystemServices;

namespace HardConfig.LIVINGROOM.WEST
{
    static class LivingRoomWestIOAssignment
    {
        public static class LivWestDigInputs
        {
            public const int indDigitalInputButtonMainDownRight = 0;
            public const int indDigitalInputButtonMainDownLeft = 1;
            public const int indDigitalInputButtonMainUpLeft = 2;
            public const int indDigitalInputButtonMainUpRight = 3;
            public const int indDigitalInputPresenceDetector = 5;
        }

        public static class LivWestDigOutputs
        {
            public const int indDigitalOutLightWindowDoorEntryLeft = 0;
            public const int indDigitalOutLightKitchenDown_1 = 1;
            public const int indDigitalOutLightKitchenDown_2 = 2;
            public const int indDigitalOutLightWindowBoardLeft = 4;
            public const int indDigitalOutLightWindowBoardRight = 5;
            public const int indDigitalOutLightWall = 7;
        }

        public const int indFirstLight = 0;
        public const int indLastLight = LivWestDigOutputs.indDigitalOutLightWall;
    }
}
