﻿using NUnit.Framework;
using System;
using LibUdp;
using LibUdp.BASIC.SEND;
using LibUdp.BASIC.CONSTANTS;
using Moq;
using TimerMockable;
using System.Timers;
using SystemServices;

namespace UdpSend_UnitTest
{
    [TestFixture]
    public class TestUdpSendPeriodic
    {
        UdpSendPeriodic TestPeriodicSender;
        Mock<ITimer> MockTestTimerPeriodic;
        Mock<ITimeUtil> MockTimeStamp;

        string[] SplittedMessage;
        string TestTimeStamp = "15052017:20h16m33s520ms";
        int AppendedIndex;

        void SetupMockedTimestamp()
        {
            MockTimeStamp = new Mock<ITimeUtil>();
            MockTimeStamp.Setup( tobj => tobj.IGetTimeStamp( ) ).Returns( TestTimeStamp );
        }

        void SetupMockedTimer()
        {
            MockTestTimerPeriodic = new Mock<ITimer>( );
            MockTestTimerPeriodic.Setup( mobj => mobj.Start() );
        }

        void RaiseTimerEvents()
        {  
            for( int idx = 0; idx < UdpSendTestParameters.TestNumberOfCounts - 1 ; idx++)
            {
                MockTestTimerPeriodic.Raise( (obj) => obj.Elapsed += null, new EventArgs() as ElapsedEventArgs );
            } 
        }

        void CleanupMessageForTesting()
        {
            SplittedMessage = null;
            AppendedIndex = 0;
        }

        void PrepareMessageForTesting()
        {
            SplittedMessage = TestPeriodicSender?.MessageWithHeader.Split( '_' );
            AppendedIndex = Convert.ToInt32( SplittedMessage[0] );
        }

        void SetupSendPeriodic()
        {
            SetupMockedTimestamp( );
            SetupMockedTimer( );
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object,
                                                      MockTimeStamp.Object
                                                    );
        }

        [Test]
        public void TestPeriodicSendingWithCountIsZero()
        {
            SetupSendPeriodic( );

            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendMessage( UdpSendTestParameters.TestStringToSend, 0);

            Assert.AreEqual( 0, TestPeriodicSender.ActualCounts);
            Assert.AreEqual(SendingCommand.eCmdIdle, TestPeriodicSender.Command);
         }

        [Test]
        public void TestPeriodicSendingWithNumberOfCounts()
        {
            SetupSendPeriodic( );
            
            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendMessage( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts);

            RaiseTimerEvents();
             
            Assert.AreEqual( UdpSendTestParameters.TestNumberOfCounts, TestPeriodicSender.ActualCounts);
        }

        [Test]
        public void TestPeriodicSendingWithNumberOfCounts_StartTimer()
        {
            SetupSendPeriodic( );

            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendMessage( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts);

            MockTestTimerPeriodic.Verify( tobj => tobj.Start(), Times.AtLeastOnce());
        
        }

        [Test]
        public void TestSendHandsOverMessage()
        {
            SetupSendPeriodic( );

            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendMessage( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts);

            Assert.AreEqual( UdpSendTestParameters.TestStringToSend, TestPeriodicSender.Message );
        }

        [Test]
        public void TestSendStatusIsIdle()
        {
            SetupSendPeriodic( );

            DataSendingEventArgs EventData = new DataSendingEventArgs();

            TestPeriodicSender.EDataSendingStatus += (sender, e) => EventData = e;

            Assert.That( EventData.Status, Is.EqualTo( SendingStatus.eIdle));
        }

        [Test]
        public void TestSendStatusIsStarted()
        {
            SetupSendPeriodic( );

            DataSendingEventArgs EventData = new DataSendingEventArgs();
            TestPeriodicSender.EDataSendingStatus += (sender, e) => EventData = e;

            TestPeriodicSender.SendMessage( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts );

            Assert.That( EventData.Status, Is.EqualTo( SendingStatus.eStarted));
            Assert.AreEqual(1, EventData.ActualCounts);
        }

        [Test]
        public void TestPeriodicSendingStatusIsFinished()
        {
            SetupSendPeriodic( );

            DataSendingEventArgs EventData = new DataSendingEventArgs();
            TestPeriodicSender.EDataSendingStatus += (sender, e) => EventData = e;
            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendMessage( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts);

            for( int idx = 0; idx < UdpSendTestParameters.TestNumberOfCounts ; idx++)
            {
                MockTestTimerPeriodic.Raise( (obj) => obj.Elapsed += null, new EventArgs() as ElapsedEventArgs );
            } 

            Assert.That( EventData.Status, Is.EqualTo( SendingStatus.eFinished));
        }

        [Test]
        public void TestPeriodicSendingStatusIsPulsing()
        {
            SetupSendPeriodic( );

            DataSendingEventArgs EventData = new DataSendingEventArgs();
            TestPeriodicSender.EDataSendingStatus += (sender, e) => EventData = e;
            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendMessage(UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts);

            for (int idx = 0; idx < UdpSendTestParameters.TestNumberOfCounts-1; idx++)
            {
                MockTestTimerPeriodic.Raise((obj) => obj.Elapsed += null, new EventArgs() as ElapsedEventArgs);
            }

            Assert.That(EventData.Status, Is.EqualTo(SendingStatus.ePulsingCounted));
        }

        [Test]
        public void TestPeriodicSendingEndless_WhenCommand_Is_Idle()
        {
            SetupSendPeriodic( );

            Assert.AreEqual(SendingCommand.eCmdIdle, TestPeriodicSender.Command);
        }

        [Test]
        public void TestPeriodicSendingEndless()
        {
            SetupSendPeriodic( );

            DataSendingEventArgs EventData = new DataSendingEventArgs();
            TestPeriodicSender.EDataSendingStatus += (sender, e) => EventData = e;
            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendPeriodicMessage(UdpSendTestParameters.TestStringToSend);

            for (int idx = 0; idx < UdpSendTestParameters.TestNumberOfCounts - 1; idx++)
            {
                MockTestTimerPeriodic.Raise((obj) => obj.Elapsed += null, new EventArgs() as ElapsedEventArgs);
            }

            Assert.That(EventData.Status, Is.EqualTo(SendingStatus.ePulsingEndless));
            Assert.AreEqual(SendingCommand.eStartEndless, TestPeriodicSender.Command);
        }

        [Test]
        public void TestPeriodicSendingEndless_Stop()
        {
            SetupSendPeriodic( );

            DataSendingEventArgs EventData = new DataSendingEventArgs();
            TestPeriodicSender.EDataSendingStatus += (sender, e) => EventData = e;
            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendPeriodicMessage(UdpSendTestParameters.TestStringToSend);

            for (int idx = 0; idx < UdpSendTestParameters.TestNumberOfCounts - 1; idx++)
            {
                MockTestTimerPeriodic.Raise((obj) => obj.Elapsed += null, new EventArgs() as ElapsedEventArgs);
            }

            TestPeriodicSender.StopSendPeriodicMessage();

            Assert.That(EventData.Status, Is.EqualTo(SendingStatus.eIdle));
            Assert.AreEqual(SendingCommand.eStop, TestPeriodicSender.Command);
        }

        [Test]
        public void TestSendMessageWithMetaData()
        {
            CleanupMessageForTesting( );

            SetupSendPeriodic( );

            TestPeriodicSender.SendMessageWithHeader( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts );
            PrepareMessageForTesting( );

            Assert.AreEqual( 1, AppendedIndex );
        }


        [Test]
        public void TestPeriodicSendingEndlessWithMetaData()
        {
            CleanupMessageForTesting( );

            SetupSendPeriodic( );

            DataSendingEventArgs EventData = new DataSendingEventArgs( );
            TestPeriodicSender.EDataSendingStatus += ( sender, e ) => EventData = e;
            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendPeriodicMessageWithHeader( UdpSendTestParameters.TestStringToSend );

            for (int idx = 2; idx < UdpSendTestParameters.TestNumberOfCounts - 1; idx++)
            {
                MockTestTimerPeriodic.Raise( ( obj ) => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
                PrepareMessageForTesting( );
                Assert.AreEqual( idx, AppendedIndex );
            }
        }
    }
}
