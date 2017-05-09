﻿using NUnit.Framework;
using System;
using LibUdp;
using LibUdp.BASIC.SEND;
using LibUdp.BASIC.CONSTANTS;
using Moq;
using TimerMockable;
using System.Timers;


namespace UdpSend_UnitTest
{
    public static class UdpSendTestParameters
    {
        public const string ValidIpAdress           = "127.0.0.1"; 
        public const string InvalidIpAdress         = "1232342.0.0.66666";
        public const string TestStringToSend        = "hello";
        public const string InvalidTestStringToSend = "";
        public const int    ValidPort               = 5000;
        public const uint   ActualCountsFakeValue   = 222;
        public const uint   TestNumberOfCounts      = 10;
    }

    [TestFixture]
    public class TestUdpSend
    {
        UdpSend TestSender;

        [Test]
        public void TestValidIpAndPort( )
        {
            TestSender = new UdpSend( UdpSendTestParameters.ValidIpAdress, UdpSendTestParameters.ValidPort );
        }

        [Test]
        public void TestInvalidIp( )
        {
            Assert.Catch<Exception>(() => new UdpSend( UdpSendTestParameters.InvalidTestStringToSend, UdpSendTestParameters.ValidPort) );
        }

        [Test]
        public void TestSendingFailed()
        {
            Assert.Catch<Exception>(() => new UdpSend( UdpSendTestParameters.ValidIpAdress, UdpSendTestParameters.ValidPort).SendString(UdpSendTestParameters.InvalidTestStringToSend));
        }
    }
}
