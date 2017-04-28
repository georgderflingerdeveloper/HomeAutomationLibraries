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
            client?.Send( data, data.Length, remoteEndPoint );
        }

        public void SendString( string message )
        {
            sendString( message );
        }
    }

    public class UdpSendPeriodic : UdpSend, IUdpSendPeriodic
    {
        ITimer _PeriodicTimer;
        ITimer _DurationTimer;
        uint           _Counts;
        private uint   _ActualCounts;
        private string _Message;

        public UdpSendPeriodic( string ip_, int port_, ITimer PeriodicTimer, ITimer DurationTimer ) : base( ip_, port_ )
        {
            _PeriodicTimer = PeriodicTimer;
            _DurationTimer = DurationTimer;
            _PeriodicTimer.Elapsed += _PeriodicTimer_Elapsed;
        }

        public void SendString( string message, uint counts )
        {
            if( counts == 0 )
            {
                _ActualCounts = 0;
                return;
            }
            _PeriodicTimer.Start( );
            _Counts = counts;
            _ActualCounts = 1;
            sendString( message );
            _Message = message;
        }

        private void _PeriodicTimer_Elapsed( object sender, ElapsedEventArgs e )
        {
            sendString( _Message );
            if( _ActualCounts >= _Counts - 1 )
            {
                _PeriodicTimer.Stop( );
            }
            _ActualCounts++;
        }

        public uint ActualCounts
        {
            get
            {
                return _ActualCounts;
            }

            set
            {
                _ActualCounts = value;
            }
        }

        public string Message { get => _Message; set => _Message = value; }
    }
}
