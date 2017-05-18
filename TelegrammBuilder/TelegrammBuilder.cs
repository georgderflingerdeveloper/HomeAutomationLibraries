﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemServices;
using HardConfig.SLEEPINGROOM;
using HardConfig.COMMON;

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
        public string MsgTelegramm { get; set; }
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
            Room = SleepingRoomDeviceNames.RoomName;
            FurnitureStatusInformation = new Dictionary<int, Furniture>
            {
                { SleepingRoomIODeviceIndices.indDigitalInputWindowWest, new Furniture{ Name = SleepingRoomDeviceNames.WindowWest_, Status = FurnitureStatus.Unknown, TimeStampWhenStatusChange = GeneralConstants.EmptyTimeStamp } },
                { SleepingRoomIODeviceIndices.indDigitalInputMansardWindowNorthLeft, new Furniture{ Name = SleepingRoomDeviceNames.MansardWindowNorthLeft, Status = FurnitureStatus.Unknown, TimeStampWhenStatusChange = GeneralConstants.EmptyTimeStamp } }
            };
        }
    }

    public delegate void NewTelegramm( object sender, TelegrammEventArgs e );

    public class MsgTelegrammBuilder : ITelegrammBuilder
    {
        ITimeUtil _TimeStamp;
        Rooms     _Rooms;
        TelegrammEventArgs TelEventArgs = new TelegrammEventArgs( );
        Telegramm _TelegrammTemplate;
        string TelegrammTrailer;
        string[] MessageTelegramm;

        public MsgTelegrammBuilder( Telegramm TelegrammTemplate, ITimeUtil TimeStamp )
        {
            _TelegrammTemplate = TelegrammTemplate;
            _TimeStamp = TimeStamp;
            MakeMsgTelegramTemplate( );
        }
        public event NewTelegramm ENewTelegramm;

        void FireTelegramm()
        {
            ENewTelegramm?.Invoke( this, TelEventArgs );
        }

        void MakeMsgTelegramTemplate()
        {
            string telegramm = "";

            foreach( KeyValuePair<int, Furniture> pairs in _TelegrammTemplate.FurnitureStatusInformation )
            {
               telegramm += pairs.Value.Name;
               telegramm += Seperators.TelegrammSeperator;
               telegramm += pairs.Value.Status;
               telegramm += Seperators.TelegrammSeperator;
               telegramm += pairs.Value.TimeStampWhenStatusChange;
               telegramm += Seperators.TelegrammSeperator;
            }

            MessageTelegramm = telegramm.Split( Seperators.TelegrammSeperator );
        }

        void ActualiseTelegramm( int numberAsKey, bool value )
        {
            TelegrammTrailer = _TelegrammTemplate.Room + Seperators.TelegrammSeperator + _TimeStamp.IGetTimeStamp( );

            int index = 0;
            foreach( string elements in MessageTelegramm )
            {
                if( MessageTelegramm[index] == _TelegrammTemplate.FurnitureStatusInformation[numberAsKey].Name )
                {
                    break;
                }
                index++;
            }

            MessageTelegramm[index] = _TelegrammTemplate.FurnitureStatusInformation[numberAsKey].Name;

            TelEventArgs.MsgTelegramm = TelegrammTrailer + Seperators.TelegrammSeperator + String.Join( "_", MessageTelegramm );

        }
        public void GotIoChange( int ionumber, bool value )
        {
            ActualiseTelegramm( ionumber, value );
            FireTelegramm( );
        }
    }
}
