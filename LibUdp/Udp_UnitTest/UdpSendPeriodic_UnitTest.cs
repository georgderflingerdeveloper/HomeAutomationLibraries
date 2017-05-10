﻿using NUnit.Framework;
using System;
using LibUdp;
using LibUdp.BASIC.SEND;
using LibUdp.BASIC.CONSTANTS;
using Moq;
using TimerMockable;
using System.Timers;
using System.CodeDom;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace UdpSend_UnitTest
{
    [TestFixture]
    public class TestUdpSendPeriodic
    {
        UdpSendPeriodic TestPeriodicSender;
        Mock<ITimer> MockTestTimerPeriodic;
        string[] SplittedMessage;
        int AppendedIndex;

        void SetupMockedTimer()
        {
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

        [Test]
        public void TestPeriodicSendingWithCountIsZero()
        {
            MockTestTimerPeriodic = new Mock<ITimer>();
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );
            
            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendMessage( UdpSendTestParameters.TestStringToSend, 0);

            Assert.AreEqual( 0, TestPeriodicSender.ActualCounts);
            Assert.AreEqual(SendingCommand.eCmdIdle, TestPeriodicSender.Command);
         }

        [Test]
        public void TestPeriodicSendingWithNumberOfCounts()
        {
            MockTestTimerPeriodic = new Mock<ITimer>();
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );
            
            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendMessage( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts);

            RaiseTimerEvents();
             
            Assert.AreEqual( UdpSendTestParameters.TestNumberOfCounts, TestPeriodicSender.ActualCounts);
        }

        [Test]
        public void TestPeriodicSendingWithNumberOfCounts_StartTimer()
        {
            MockTestTimerPeriodic = new Mock<ITimer>();
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                     UdpSendTestParameters.ValidPort,
                                                     MockTestTimerPeriodic.Object
                                                    );

            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendMessage( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts);

            MockTestTimerPeriodic.Verify( tobj => tobj.Start(), Times.AtLeastOnce());
        
        }

        [Test]
        public void TestSendHandsOverMessage()
        {
            MockTestTimerPeriodic = new Mock<ITimer>();
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );

            TestPeriodicSender.ActualCounts = UdpSendTestParameters.ActualCountsFakeValue;

            TestPeriodicSender.SendMessage( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts);

            Assert.AreEqual( UdpSendTestParameters.TestStringToSend, TestPeriodicSender.Message );
        }

        [Test]
        public void TestSendStatusIsIdle()
        {
            MockTestTimerPeriodic = new Mock<ITimer>(); 
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );

            DataSendingEventArgs EventData = new DataSendingEventArgs();

            TestPeriodicSender.EDataSendingStatus += (sender, e) => EventData = e;

            Assert.That( EventData.Status, Is.EqualTo( SendingStatus.eIdle));
        }

        [Test]
        public void TestSendStatusIsStarted()
        {
            MockTestTimerPeriodic = new Mock<ITimer>();
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );
            DataSendingEventArgs EventData = new DataSendingEventArgs();
            TestPeriodicSender.EDataSendingStatus += (sender, e) => EventData = e;

            TestPeriodicSender.SendMessage( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts );

            Assert.That( EventData.Status, Is.EqualTo( SendingStatus.eStarted));
            Assert.AreEqual(1, EventData.ActualCounts);
        }

        [Test]
        public void TestPeriodicSendingStatusIsFinished()
        {
            MockTestTimerPeriodic = new Mock<ITimer>();
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );

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
            MockTestTimerPeriodic = new Mock<ITimer>();
            TestPeriodicSender = new UdpSendPeriodic(UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );

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
            MockTestTimerPeriodic = new Mock<ITimer>();
            TestPeriodicSender = new UdpSendPeriodic(UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );


            Assert.AreEqual(SendingCommand.eCmdIdle, TestPeriodicSender.Command);
        }

        [Test]
        public void TestPeriodicSendingEndless()
        {
            MockTestTimerPeriodic = new Mock<ITimer>();
            TestPeriodicSender = new UdpSendPeriodic(UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );

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
            MockTestTimerPeriodic = new Mock<ITimer>();
            TestPeriodicSender = new UdpSendPeriodic(UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );

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
            MockTestTimerPeriodic = new Mock<ITimer>( );
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );

            TestPeriodicSender.SendMessageWithHeader( UdpSendTestParameters.TestStringToSend, UdpSendTestParameters.TestNumberOfCounts );
            PrepareMessageForTesting( );

            Assert.AreEqual( 1, AppendedIndex );
        }


        [Test]
        public void TestPeriodicSendingEndlessWithMetaData()
        {
            CleanupMessageForTesting( );
            MockTestTimerPeriodic = new Mock<ITimer>( );
            TestPeriodicSender = new UdpSendPeriodic( UdpSendTestParameters.ValidIpAdress,
                                                      UdpSendTestParameters.ValidPort,
                                                      MockTestTimerPeriodic.Object
                                                    );

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
