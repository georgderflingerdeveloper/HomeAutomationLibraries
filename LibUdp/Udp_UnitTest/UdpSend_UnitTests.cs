using NUnit.Framework;
using System;
using LibUdp;
using LibUdp.BASIC.SEND;
using LibUdp.BASIC.CONSTANTS;
namespace UdpSend_UnitTest
{
    [TestFixture( )]
    public class TestUdpSend
    {
        UdpSend TestSender;
        string ValidIpAdress = "127.0.0.1";
        string InvalidIpAdress = "1232342.0.0.66666";
        int ValidPort = 5000;


        [Test( )]
        public void TestValidIpAndPort( )
        {
            TestSender = new UdpSend( ValidIpAdress, ValidPort );
        }

        [Test( )]
        public void TestInvalidIp( )
        {
            try
            {
               TestSender = new UdpSend( InvalidIpAdress, ValidPort );
            }
            catch( Exception ex )
            {
               Assert.That( ex.Message, Is.EqualTo(BasicErrorMessage.InvalidIpAdress));
            }
        }

    }
}
