using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardConfig.COMMON
{
    public static class GeneralConstants
    {
        public const double TimerDisabled = 0;
        public const int NumberOfOutputsIOCard = 16;
        public const int NumberOfInputsIOCard = 16;
        public const int DeviceNotFound = -1;
        public const string DigitalInput = "DigitalInput:";
        public const string DigitalOutput = "DigitalOutput:";
        // for some string operations : is intepreted as delimiter - so we don´t need this in certain cases!
        public const string DigitalInput_ = "DigitalInput";
        public const string DigitalOutput_ = "DigitalOutput";
        public const bool ON = true;
        public const bool OFF = false;
        public const bool STARTAUTOMATICOFF = false;
        public const bool STOPAUTOMATICOFF = false;
        public const double DURATION_COMMONTICK = 1000.0;
        public const string SlashUsedInLinux = "//";
        public const string BackSlash = "\\";
        public const string EmptyTimeStamp = "00000000:00h00m00s000ms";
    }
}
