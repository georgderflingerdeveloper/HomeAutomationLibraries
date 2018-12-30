using System;
using System.Timers;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LibUdp.BASIC.CONSTANTS;

namespace LibUdp.BASIC.RECEIVE
{
    public class DataReceivingEventArgs : EventArgs
    {
        public string Message { get; set; }
        public string Adress  { get; set; }
        public string Port    { get; set; }
    }

    public delegate void DataReceived( object sender, DataReceivingEventArgs e );

    public class UdpReceive : IUdpReceive
    {
        Thread     receiveThread;
        UdpClient  client;
        IPEndPoint anyIP;
        DataReceivingEventArgs ReceivingArgs = new DataReceivingEventArgs();
        int port; 

        public UdpReceive( int port_ )
        {
            port = port_;
            ReceivingArgs.Port = port.ToString();
            client = new UdpClient( port );
            anyIP  = new IPEndPoint( IPAddress.Any, port );
            ReceivingArgs.Adress = anyIP.ToString();
            receiveThread = new Thread( new ThreadStart( ReceiveData ) );
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        public string lastReceivedUDPPacket = "";
        public string allReceivedUDPPackets = "";
        string _receivedText;

        public event DataReceived EDataReceived;

        private void ReceiveData()
        {
            _receivedText = "";
            if( client != null )
            {
                while (true)
                {
                    try
                    {
                        if( anyIP != null )
                        {
                            byte[] data = client.Receive( ref anyIP );

                            if( (data != null) && (data.Length > 0) )
                            {
                                _receivedText = Encoding.UTF8.GetString(data);
                                if( !String.IsNullOrEmpty( _receivedText ) )
                                {
                                    ReceivingArgs.Message = _receivedText;
                                    EDataReceived?.Invoke( this, ReceivingArgs );
                                }
                                lastReceivedUDPPacket = _receivedText;
                                allReceivedUDPPackets = allReceivedUDPPackets + _receivedText;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception( BasicErrorMessage.ReceiveDataFailed, ex );
                    }
                }
            }
        }

        public string ReceivedText
        {
            get
            {
                return _receivedText;
            }
        }

        public void Abort()
        {
            if( client != null )
            {
                client.Close();
                receiveThread.Abort();
            }
        }
    }
}
