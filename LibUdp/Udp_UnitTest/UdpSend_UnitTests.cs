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


    [TestFixture]
    public class TestUdpSendPeriodic
    {
        Mock<ITimer> MockTestTimerPeriodic = new Mock<ITimer>();
        Mock<ITimer> MockTestTimerDuration = new Mock<ITimer>();
        UdpSendPeriodic TestPeriodicSender;


        [Test]
        public void TestPeriodicSendingWithCountIsZero()
        {
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                                      UdpSendTestParameters.ValidPort,
                                                                      MockTestTimerPeriodic.Object,
                                                                      MockTestTimerDuration.Object
                                                                    );
            
            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendString( UdpSendTestParameters.TestStringToSend, 0);

            Assert.AreEqual( 0, TestPeriodicSender.ActualCounts);
        }

        [Test]
        public void TestPeriodicSending()
        {
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object,
                                                      MockTestTimerDuration.Object
                                                    );
            
            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendString( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts);

            for( int idx = 0; idx < UdpSendTestParameters.TestNumberOfCounts - 1 ; idx++)
            {
                MockTestTimerPeriodic.Raise( (obj) => obj.Elapsed += null, new EventArgs() as ElapsedEventArgs );
            }

            Assert.AreEqual( UdpSendTestParameters.TestNumberOfCounts, TestPeriodicSender.ActualCounts);
        }

    }
}
