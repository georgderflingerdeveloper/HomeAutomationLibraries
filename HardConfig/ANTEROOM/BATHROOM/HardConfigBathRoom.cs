using System;
using SystemServices;

namespace HardConfig.ANTEROOM.BATHROOM
{
    static class BathRoomIODeviceIndices
    {
        public const int indDigitalOutputBathRoomFirstHeater = 12;
        public const int indDigitalOutputBathRoomLastHeater = 12;
        public const int indDigitalOutputBathRoomHeater = 12;
        //public const int indDigitalOutputBathRoomHeater      = ??;
    }

    static class ParametersHeaterControlBathRoom
    {
        static readonly public double TimeDemandForHeaterAutomaticOff = new TimeSpan( 2, 0, 0, 0, 0 ).TotalMilliseconds;
    }

    static class ParametersLightControlBathRoom
    {
        static readonly public double TimeDemandForAutomaticOffBath = TimeConverter.ToMiliseconds( 1, 0, 0 );
    }

    static class BathRoomIOLightIndices
    {
        public const int indFirstBathRoom = 7;
        public const int indLastBathRoom = 11;
    }

    static class BathRoomIOLightIndexNaming
    {
        public const int MiddleLight = BathRoomIOLightIndices.indFirstBathRoom;
        public const int RBG_PanelOverBath = BathRoomIOLightIndices.indLastBathRoom;
    }

}
