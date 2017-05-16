using System;
using System.Timers;
using LibUdp.BASIC.SEND.INTERFACE;
using TimerMockable;
using SystemServices;

namespace LibUdp.BASIC.SEND
{
    public static class Formatting
    {
        public static string MessageSeperator = "_";
    }

    public enum SendingStatus
    {
        eIdle,
        eStarted,
        ePulsingCounted,
        ePulsingEndless,
        eFinished,
        eInvalid
    }

    public enum SendingCommand
    {
        eCmdIdle,
        eStop,
        eStartEndless
    }

    public class DataSendingEventArgs : EventArgs
    {
        public uint ActualCounts { get; set; }
        public SendingStatus Status { get; set; }
    }

    public class UdpSendPeriodic : UdpSend, IUdpSendPeriodic
    {
        ITimer _PeriodicTimer;
        ITimeUtil _TimeUtil;
        uint _Counts;
        private uint _ActualCounts;
        private string _Message;
        SendingCommand _Command;
        string _MessageWithHeader;
        bool _AppendMetaData = false;
        DataSendingEventArgs _DataSendingEventArgs = new DataSendingEventArgs();
        public delegate void DataSendingStatus(object sender, DataSendingEventArgs e);
        public event DataSendingStatus EDataSendingStatus;

        public UdpSendPeriodic(string ip_, int port_, ITimer PeriodicTimer, ITimeUtil TimeUtil ) : base(ip_, port_)
        {
            _PeriodicTimer = PeriodicTimer;
            _TimeUtil = TimeUtil;
            _PeriodicTimer.Elapsed += _PeriodicTimer_Elapsed;
            FireSendingStatus( SendingStatus.eIdle );
        }

        void FireSendingStatus( SendingStatus status )
        {
            _DataSendingEventArgs.Status = status;
            EDataSendingStatus?.Invoke(this, _DataSendingEventArgs);
        }

        void RestartPeriodicTimer()
        {
            _PeriodicTimer.Stop();
            _PeriodicTimer.Start();
        }

        void PreparePeriodicSending( uint counts )
        {
            _PeriodicTimer.Start( );
            _Counts = counts;
            _ActualCounts = 1;
            _DataSendingEventArgs.ActualCounts = _ActualCounts;
        }

        bool IsCountZero( uint counts )
        {
            _Command = SendingCommand.eCmdIdle;
            if (counts == 0)
            {
                _ActualCounts = 0;
                return true;
            }
            return false;
        }

        string AppendHeader( string message )
        {
            return (   _ActualCounts.ToString( ) 
                     +  Formatting.MessageSeperator 
                     + _TimeUtil.IGetTimeStamp() 
                     +  Formatting.MessageSeperator
                     +  message );
        }

 
        public void SendMessage( string message, uint counts )
        {
            if( IsCountZero( counts ) )
            {
                return;
            }
            PreparePeriodicSending( counts );
            _Message = message;
            sendMessage( _Message );
            FireSendingStatus( SendingStatus.eStarted );
            return;
        }

        public void SendMessageWithHeader( string message, uint counts )
        {
            _AppendMetaData = true;
            if (IsCountZero( counts ))
            {
                return;
            }
            PreparePeriodicSending( counts );
            _Message = message;
            _MessageWithHeader = AppendHeader( _Message );
            sendMessage( _MessageWithHeader );
            FireSendingStatus( SendingStatus.eStarted );
        }

        public void SendPeriodicMessage( string message )
        {
            _PeriodicTimer.Start();
            _Message = message;
            sendMessage( _Message );
            _Command = SendingCommand.eStartEndless;
            FireSendingStatus( SendingStatus.eStarted );
        }

        public void SendPeriodicMessageWithHeader( string message )
        {
            _AppendMetaData = true;
            _PeriodicTimer.Start( );
            _ActualCounts = 1;
            _Message = message;
            _MessageWithHeader = AppendHeader( _Message );
            sendMessage( _MessageWithHeader );
            _Command = SendingCommand.eStartEndless;
            FireSendingStatus( SendingStatus.eStarted );
        }

        public void StopSendPeriodicMessage()
        {
            _PeriodicTimer.Stop();
            FireSendingStatus(SendingStatus.eIdle);
            _Command = SendingCommand.eStop;
            _AppendMetaData = false;
        }

        void SendMessageWithTimerRestart()
        {
            _ActualCounts++;
            if( _AppendMetaData )
            {
                _MessageWithHeader = AppendHeader( _Message );
                sendMessage( _MessageWithHeader );
            }
            else
            {
                sendMessage( _Message );
            }
            _DataSendingEventArgs.ActualCounts = _ActualCounts;
            RestartPeriodicTimer();
        }

        private void _PeriodicTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_Command == SendingCommand.eStartEndless)
            {
                SendMessageWithTimerRestart();
                FireSendingStatus(SendingStatus.ePulsingEndless);
                return;
            }

            if (_ActualCounts < _Counts)
            {
                SendMessageWithTimerRestart();
                FireSendingStatus(SendingStatus.ePulsingCounted);
            }
            else
            {
                _DataSendingEventArgs.ActualCounts = _ActualCounts = 0;
                _PeriodicTimer.Stop();
                FireSendingStatus(SendingStatus.eFinished);
            }
        }

        public uint ActualCounts { get => _ActualCounts; set => _ActualCounts = value; }
        public string Message { get => _Message; set => _Message = value; }
        public SendingCommand Command { get => _Command; set => _Command = value; }
        public string MessageWithHeader { get => _MessageWithHeader; set => _MessageWithHeader = value; }
   }
}
