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
        string TestStringToSend = "hello";
        string InvalidTestStringToSend = "";
        int ValidPort = 5000;


        [Test( )]
        public void TestValidIpAndPort( )
        {
            TestSender = new UdpSend( ValidIpAdress, ValidPort );
        }

        [Test( )]
        public void TestInvalidIp( )
        {
           Assert.Catch<Exception>(() => new UdpSend(InvalidIpAdress, ValidPort));
        }

        [Test()]
        public void TestSendingFailed()
        {
            Assert.Catch<Exception>(() => new UdpSend(ValidIpAdress, ValidPort).SendString(InvalidTestStringToSend));
        }
    }
}
