using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemServices;
using HardConfig.SLEEPINGROOM;

namespace TelegrammBuilder
{
    public enum Rooms
    {
        eLivingRoom,
        eAnteRoom,
        eSleepingRoom,
        eBathRoom,
        eGallery,
        eRoof
    }

    public class TelegrammEventArgs : EventArgs
    {
        string MsgTelegramm;
    }

    public static class FurnitureStatus
    {
        public static string Open       = "OFFEN";
        public static string Closed     = "GESCHLOSSEN";
        public static string TippedOver = "GEKIPPT";
        public static string Unknown    = "UNKNOWN";
    }

    public class Furniture
    {
        public string Name { get; set; }
        public string TimeStampWhenStatusChange { get; set; }
        public string Status { get; set; }
    }

    public class Telegramm
    {
        public string Room { get; set; }
        public Dictionary<int, Furniture> FurnitureStatusInformation { get; set; }
    }

    public class SleepingRoomTelegramm : Telegramm
    {
        public SleepingRoomTelegramm()
        {
            FurnitureStatusInformation = new Dictionary<int, Furniture>
            {
                { SleepingRoomIODeviceIndices.indDigitalInputWindowWest, new Furniture{ Name = "FENSTERWEST", Status = FurnitureStatus.Unknown, TimeStampWhenStatusChange = ""} }
            };
            
        }
    }

    public delegate void NewTelegramm( object sender, TelegrammEventArgs e );

    public class TelegrammBuilder : ITelegrammBuilder
    {
        ITimeUtil _TimeStamp;
        Rooms _Rooms;
        public TelegrammBuilder( Telegramm TelegrammTemplate, ITimeUtil TimeStamp )
        {

        }
        public event NewTelegramm ENewTelegramm;
        public void GotIoChange( int index, bool value )
        {
        }
    }
}
