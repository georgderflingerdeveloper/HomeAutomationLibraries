using System;
using System.Timers;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LibUdp.BASIC.SEND.INTERFACE;
using LibUdp.BASIC.CONSTANTS;
using TimerMockable;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;


namespace LibUdp.BASIC.SEND
{
    public class UdpSend : IUdpSend
    {
        IPEndPoint remoteEndPoint;
        UdpClient client;
        IPAddress Adress;

        public UdpSend( string ip_, int port_ )
        {
            Adress = IPAddress.Parse( ip_ );
            remoteEndPoint = new IPEndPoint( Adress, port_ );
            client = new UdpClient( );
        }

        protected void sendString( string message )
        {
            if( String.IsNullOrWhiteSpace( message ) )
            {
                throw (new Exception( BasicErrorMessage.InvalidSendString ));
            }
            byte[] data = Encoding.UTF8.GetBytes( message );
            client?.SendAsync( data, data.Length, remoteEndPoint );
        }

        public void SendString( string message )
        {
            sendString( message );
        }
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
        public uint            ActualCounts { get; set; }
        public SendingStatus   Status       { get; set; }
    }

    public class UdpSendPeriodic : UdpSend, IUdpSendPeriodic
    {
        ITimer         _PeriodicTimer;
        uint           _Counts;
        private uint   _ActualCounts;
        private string _Message;
        SendingCommand _Command;
        DataSendingEventArgs _DataSendingEventArgs = new DataSendingEventArgs();
        public delegate void DataSendingStatus( object sender, DataSendingEventArgs e );
        public event DataSendingStatus EDataSendingStatus;

        void FireSendingStatus( SendingStatus status )
        {
            _DataSendingEventArgs.Status = status;
            EDataSendingStatus?.Invoke(this, _DataSendingEventArgs);
        }
 
        void RestartPeriodicTimer( )
        {
            _PeriodicTimer.Stop();
            _PeriodicTimer.Start();
        }

        public UdpSendPeriodic( string ip_, int port_, ITimer PeriodicTimer ) : base( ip_, port_ )
        {
            _PeriodicTimer = PeriodicTimer;
            _PeriodicTimer.Elapsed += _PeriodicTimer_Elapsed;
            FireSendingStatus( SendingStatus.eIdle );
        }

        public void SendString( string message, uint counts )
        {
            _Command = SendingCommand.eCmdIdle;
           if ( counts == 0 )
            {
                _ActualCounts = 0;
                return;
            }
            _PeriodicTimer.Start( );
            _Counts       = counts;
            _ActualCounts = 1;
            _DataSendingEventArgs.ActualCounts = _ActualCounts;
            sendString( message );
            _Message = message;
            FireSendingStatus( SendingStatus.eStarted );
         }

        public void SendStringCyclic( string message )
        {
            _PeriodicTimer.Start();
            _Message = message;
            sendString( message );
            _Command = SendingCommand.eStartEndless;
            FireSendingStatus(SendingStatus.eStarted);
        }

        public void StopSendStringCyclic( )
        {
            _PeriodicTimer.Stop();
            FireSendingStatus( SendingStatus.eIdle );
            _Command = SendingCommand.eStop;
        }

        void SendCountWithTimerRestart( )
        {
            sendString( _Message );
            _ActualCounts++;
            _DataSendingEventArgs.ActualCounts = _ActualCounts;
            RestartPeriodicTimer();
        }

        private void _PeriodicTimer_Elapsed( object sender, ElapsedEventArgs e )
        {

            if( _Command == SendingCommand.eStartEndless )
            {
                SendCountWithTimerRestart( );
                FireSendingStatus(SendingStatus.ePulsingEndless);
                return;
            }

            if( _ActualCounts < _Counts )
            {
                SendCountWithTimerRestart( );
                FireSendingStatus( SendingStatus.ePulsingCounted );
            }
            else
            {
                _DataSendingEventArgs.ActualCounts = _ActualCounts = 0;
                _PeriodicTimer.Stop();
                FireSendingStatus( SendingStatus.eFinished );
            }
        }

        public uint           ActualCounts{ get => _ActualCounts; set => _ActualCounts = value; }
        public string         Message     { get => _Message;      set => _Message      = value; }
        public SendingCommand Command     { get => _Command;      set => _Command      = value; }
    }
}
