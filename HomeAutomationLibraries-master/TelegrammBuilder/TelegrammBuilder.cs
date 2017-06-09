﻿﻿using System;
using System.Collections.Generic;
using SystemServices;
using HardConfig.SLEEPINGROOM;
using HardConfig.COMMON;

namespace TelegrammBuilder
{
    public class TelegrammEventArgs : EventArgs
    {
        public string MsgTelegramm { get; set; }
    }

    public static class DeviceStatus
    {
        public static string Open       = "OFFEN";
        public static string Closed     = "GESCHLOSSEN";
        public static string TippedOver = "GEKIPPT";
        public static string Unknown    = "UNBEKANNT";
    }

    public static class TelegrammTypes
    {
        public static string WindowInformation = "FENSTERINFORMATION";
        public static string DoorInformation   = "TÜRINFORMATION";
    }

    public static class TelegrammStatus
    {
        public static string TelegramConfigurationMismatch = "Mismatch in telegram configuration !";
    }

    public class Device
    {
        public string Name { get; set; }
        public string TimeStampWhenStatusChange { get; set; }
        public string Status { get; set; }
    }

    public class Telegramm
    {
        public string Type { get; set; }
        public string Room { get; set; }
        public Dictionary<int, Device> DeviceStatusInformation { get; set; }
    }

    public class SleepingRoomTelegramm : Telegramm
    {
        public SleepingRoomTelegramm()
        {
            Room = SleepingRoomDeviceNames.RoomName;
            Type = TelegrammTypes.WindowInformation;
            DeviceStatusInformation = new Dictionary<int, Device>
            {
                { SleepingRoomIODeviceIndices.indDigitalInputWindowWest,
                    new Device  {
                                   Name                      = SleepingRoomDeviceNames.WindowWest_,
                                   Status                    = DeviceStatus.Unknown,
                                   TimeStampWhenStatusChange = GeneralConstants.EmptyTimeStamp
                                }
                },
                { SleepingRoomIODeviceIndices.indDigitalInputMansardWindowNorthLeft,
                    new Device  {
                                   Name                      = SleepingRoomDeviceNames.MansardWindowNorthLeft,
                                   Status                    = DeviceStatus.Unknown,
                                   TimeStampWhenStatusChange = GeneralConstants.EmptyTimeStamp
                                }
                }
            };
        }
    }

    public delegate void NewTelegramm( object sender, TelegrammEventArgs e );

    public class MsgTelegrammBuilder : ITelegrammBuilder
    {
        ITimeUtil _TimeStamp;
        TelegrammEventArgs TelEventArgs = new TelegrammEventArgs( );
        Telegramm _TelegrammTemplate;
        string TelegrammTrailer;
        string[] TelegrammContainer;
        int StatusPosition = 1;
        int TimestampPosition = 2;

        public MsgTelegrammBuilder( Telegramm TelegrammTemplate, ITimeUtil TimeStamp )
        {
            _TelegrammTemplate = TelegrammTemplate;
            _TimeStamp = TimeStamp;
            MakeMsgTelegramTemplate( );
        }

        public event NewTelegramm ENewTelegramm;

        void FireTelegramm( )
        {
            ENewTelegramm?.Invoke( this, TelEventArgs );
        }

        void MakeMsgTelegramTemplate( )
        {
            string telegramm = "";
            int index = 0;

            foreach( KeyValuePair<int, Device> pairs in _TelegrammTemplate.DeviceStatusInformation )
            {
               telegramm += pairs.Value.Name;
               telegramm += Seperators.TelegrammSeperator;
               telegramm += pairs.Value.Status;
               telegramm += Seperators.TelegrammSeperator;
               telegramm += pairs.Value.TimeStampWhenStatusChange;
               index++;
               if( index < _TelegrammTemplate.DeviceStatusInformation.Count )
               {
                   telegramm += Seperators.TelegrammSeperator;
               }
            }

            TelegrammContainer = telegramm.Split( Seperators.TelegrammSeperator );
        }

        void ActualiseTelegramm( int numberAsKey, bool value )
        {
            TelegrammTrailer = _TelegrammTemplate.Room + Seperators.TelegrammSeperatorStr + _TelegrammTemplate.Type;
            TelEventArgs.MsgTelegramm = null;

            if( _TelegrammTemplate.DeviceStatusInformation.ContainsKey( numberAsKey ) )
            {
                int index = 0;
                foreach( string elements in TelegrammContainer )
                {
                    if( TelegrammContainer[index] == _TelegrammTemplate.DeviceStatusInformation[numberAsKey].Name )
                    {
                        break;
                    }
                    index++;
                }

                TelegrammContainer[index]                     = _TelegrammTemplate.DeviceStatusInformation[numberAsKey].Name;
                TelegrammContainer[index + StatusPosition]    = _TelegrammTemplate.DeviceStatusInformation[numberAsKey].Status = value ? DeviceStatus.Open : DeviceStatus.Closed;
                TelegrammContainer[index + TimestampPosition] = _TelegrammTemplate.DeviceStatusInformation[numberAsKey].TimeStampWhenStatusChange = _TimeStamp.IGetTimeStamp( );

                TelEventArgs.MsgTelegramm = TelegrammTrailer + Seperators.TelegrammSeperator + String.Join( Seperators.TelegrammSeperatorStr, TelegrammContainer );
            }
            else
            {
                TelEventArgs.MsgTelegramm = TelegrammStatus.TelegramConfigurationMismatch;
            }

        }

        public void GotIoChange( int ionumber, bool value )
        {
            ActualiseTelegramm( ionumber, value );
            FireTelegramm( );
        }
    }
}
