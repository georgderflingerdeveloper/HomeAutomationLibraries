using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemServices;

namespace TelegrammBuilder
{
    public class TelegrammEventArgs : EventArgs
    {
        string MsgTelegramm;
    }

    public static class FurnitureStatus
    {
        public static string Open   = "OPEN";
        public static string Closed = "CLOSED";
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

    public delegate void NewTelegramm( object sender, TelegrammEventArgs e );

    public class TelegrammBuilder : ITelegrammBuilder
    {
        ITimeUtil _TimeStamp;
        public TelegrammBuilder( ITimeUtil TimeStamp )
        {

        }
        public event NewTelegramm ENewTelegramm;
        public void GotIoChange( int index, bool value )
        {
        }
    }
}
