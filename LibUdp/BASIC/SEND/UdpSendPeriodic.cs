using System;
using System.Timers;
using LibUdp.BASIC.SEND.INTERFACE;
using TimerMockable;

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
        uint _Counts;
        private uint _ActualCounts;
        private string _Message;
        SendingCommand _Command;
        string _MessageWithMetaData;
        bool _AppendMetaData = false;
        DataSendingEventArgs _DataSendingEventArgs = new DataSendingEventArgs();
        public delegate void DataSendingStatus(object sender, DataSendingEventArgs e);
        public event DataSendingStatus EDataSendingStatus;

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

        void PrepareSending( uint counts )
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

        string AppendMetaData( string message )
        {
            return ( _ActualCounts.ToString( ) + Formatting.MessageSeperator + message );
        }

        public UdpSendPeriodic(string ip_, int port_, ITimer PeriodicTimer) : base(ip_, port_)
        {
            _PeriodicTimer = PeriodicTimer;
            _PeriodicTimer.Elapsed += _PeriodicTimer_Elapsed;
            FireSendingStatus(SendingStatus.eIdle);
        }
 
        public void SendString( string message, uint counts )
        {
            if( IsCountZero( counts ) )
            {
                return;
            }
            PrepareSending( counts );
            _Message = message;
            sendString( _Message );
            FireSendingStatus( SendingStatus.eStarted );
        }

        public void SendStringWithMetaData( string message, uint counts )
        {
            _AppendMetaData = true;
            if (IsCountZero( counts ))
            {
                return;
            }
            PrepareSending( counts );
            _Message = message;
            _MessageWithMetaData = AppendMetaData( _Message );
            sendString( _MessageWithMetaData );
            FireSendingStatus( SendingStatus.eStarted );
        }

        public void SendStringCyclic( string message )
        {
            _PeriodicTimer.Start();
            _Message = message;
            sendString( _Message );
            _Command = SendingCommand.eStartEndless;
            FireSendingStatus( SendingStatus.eStarted );
        }

        public void SendStringCyclicWithMetaData( string message )
        {
            _AppendMetaData = true;
            _PeriodicTimer.Start( );
            _ActualCounts = 1;
            _Message = message;
            _MessageWithMetaData = AppendMetaData( _Message );
            sendString( _MessageWithMetaData );
            _Command = SendingCommand.eStartEndless;
            FireSendingStatus( SendingStatus.eStarted );
        }

        public void StopSendStringCyclic()
        {
            _PeriodicTimer.Stop();
            FireSendingStatus(SendingStatus.eIdle);
            _Command = SendingCommand.eStop;
            _AppendMetaData = false;
        }

        void SendCountWithTimerRestart()
        {
            _ActualCounts++;
            if( _AppendMetaData )
            {
                _MessageWithMetaData = AppendMetaData( _Message );
                sendString( _MessageWithMetaData );
            }
            else
            {
                sendString( _Message );
            }
            _DataSendingEventArgs.ActualCounts = _ActualCounts;
            RestartPeriodicTimer();
        }

        private void _PeriodicTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_Command == SendingCommand.eStartEndless)
            {
                SendCountWithTimerRestart();
                FireSendingStatus(SendingStatus.ePulsingEndless);
                return;
            }

            if (_ActualCounts < _Counts)
            {
                SendCountWithTimerRestart();
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
        public string MessageWithMetaData { get => _MessageWithMetaData; set => _MessageWithMetaData = value; }
    }
}
