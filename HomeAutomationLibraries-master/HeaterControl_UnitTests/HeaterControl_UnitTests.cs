using NUnit.Framework;
using HomeAutomationHeater;
using TimerMockable;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeaterControl_UnitTests
{
    [TestFixture]
    public class HeaterControl_UnitTests
    {
        HeaterController TestController;
        HeaterStatus TestStatus;
        Mock<ITimer> MockedStartController;
        Mock<ITimer> MockedStopController;

        void FakeInitialStatusForTesting( )
        {
            if( TestStatus != null )
            {
                TestStatus.ActualControllerState = HeaterStatus.ControllerState.InvalidForTesting;
                TestStatus.ActualOperationState = HeaterStatus.OperationState.InvalidForTesting;
            }
        }

        void SetupTest()
        {
            MockedStartController = new Mock<ITimer>( );
            MockedStopController  = new Mock<ITimer>( );
            ITimer StartController = MockedStartController.Object;
            ITimer StopController = MockedStopController.Object;
            TestController = new HeaterController( StartController, StopController );
            TestStatus = new HeaterStatus( );
        }

        void CleanUpTest()
        {
            MockedStartController = null;
            MockedStopController = null;
            TestController = null;
            TestStatus = null;
        }

        [SetUp]
        public void SetupTests()
        {
           SetupTest( );
        }

        [Test]
        public void TestCase_HeaterIsOff_StatusCheck()
        {
           FakeInitialStatusForTesting( );

           bool IsOn = true;

           TestController.EActivityChanged += ( sender, e ) =>
           {
               TestStatus = e.Status;
               IsOn = e.TurnOn;
           };

           TestController.Stop( );

           Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOff, TestStatus.ActualControllerState );
           Assert.AreEqual( HeaterStatus.OperationState.Idle,             TestStatus.ActualOperationState );
           Assert.IsFalse( IsOn );
        }

        [Test]
        public void TestCase_HeaterIsOn_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOn, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.RegularOperation, TestStatus.ActualOperationState );
            Assert.IsTrue( IsOn );
        }

        [Test]
        public void TestCase_HeaterIsPausedWhenStarted_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );
            TestController.Pause( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsPaused, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.RegularOperation, TestStatus.ActualOperationState );
            Assert.IsFalse( IsOn );
        }

        [Test]
        public void TestCase_HeaterIsResumed_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );
            TestController.Pause( );
            TestController.Resume( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOn, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.RegularOperation, TestStatus.ActualOperationState );
            Assert.IsTrue( IsOn );
        }

        [Test]
        public void TestCase_InitialToggle_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Toggle( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOn, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.RegularOperation, TestStatus.ActualOperationState );
            Assert.IsTrue( IsOn );
        }

        [Test]
        public void TestCase_ToggleAfterStarted_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );
            TestController.Toggle( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOff, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.Idle, TestStatus.ActualOperationState );
            Assert.IsFalse( IsOn );
        }

        [Test]
        public void TestCase_ToggleAfterStopped_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Stop( );
            TestController.Toggle( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOn, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.RegularOperation, TestStatus.ActualOperationState );
            Assert.IsTrue( IsOn );
        }

        [Test]
        public void TestCase_ToggleStartStop_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Toggle( ); // start
            TestController.Toggle( ); // stop

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOff, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.Idle, TestStatus.ActualOperationState );
            Assert.IsFalse( IsOn );
        }

        [Test]
        public void TestCase_ToggleStartStopStart_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Toggle( ); // start
            TestController.Toggle( ); // stop
            TestController.Toggle( ); // start

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOn, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.RegularOperation, TestStatus.ActualOperationState );
            Assert.IsTrue( IsOn );
        }


        [TearDown]
        public void TearDownTests()
        {
            CleanUpTest( );
        }
    }
}
