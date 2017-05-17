using System;
using SystemServices;

namespace HardConfig.ANTEROOM.WASHROOM
{
    static class ParametersWashRoomControl
    {
        static readonly public double TimeDemandForFanOn = TimeConverter.ToMiliseconds( 2 );
        static readonly public double TimeDemandForFanAutomaticOff = TimeConverter.ToMiliseconds( 15, 0 );
    }
    static class ParametersLightControlWashRoom
    {
        static readonly public double TimeDemandForAutomaticOffWashRoom = TimeConverter.ToMiliseconds( 20, 0 );
    }
    static class WashRoomIODeviceIndices
    {
        public const int indDigitalOutputWashRoomLight = 5;
        public const int indDigitalOutputWashRoomFan = 12;
    }

}
