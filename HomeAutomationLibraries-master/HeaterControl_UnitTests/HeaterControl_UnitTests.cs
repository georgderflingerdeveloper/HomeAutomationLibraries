using NUnit.Framework;
using TimerMockable;
using Moq;
using System;
using System.Timers;
using HomeAutomationHeater;
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
        Mock<ITimer> MockedStartStopController;
        Mock<ITimer> MockedDelayControllerPause;
        Mock<ITimer> MockedToggeling;

        void FakeInitialStatusForTesting()
        {
            if (TestStatus != null)
            {
                TestStatus.ActualControllerState = HeaterStatus.ControllerState.InvalidForTesting;
                TestStatus.ActualOperationState = HeaterStatus.OperationState.InvalidForTesting;
            }
        }

        void SetupTest()
        {
            MockedStartStopController = new Mock<ITimer>( );
            MockedDelayControllerPause = new Mock<ITimer>( );
            MockedToggeling = new Mock<ITimer>( );
            ITimer StartStopController = MockedStartStopController.Object;
            ITimer PauseController = MockedDelayControllerPause.Object;
            ITimer TimerForToggeling = MockedToggeling.Object;
            TestController = new HeaterController( new HeaterParameters( ),  PauseController, TimerForToggeling );
            TestStatus = new HeaterStatus( );
        }

        void CleanUpTest()
        {
            MockedStartStopController = null;
            MockedDelayControllerPause = null;
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

            HeaterStatus ReturnedTestedStatus = TestController.GetStatus( );
            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOff, ReturnedTestedStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.Idle, ReturnedTestedStatus.ActualOperationState );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOff, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.Idle, TestStatus.ActualOperationState );
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

        [Test]
        public void TestCase_InitialToggleWithDelayTimeTurningOn_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.DelayedToggle( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOff, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.InformationAction.TurningOn, TestStatus.ActualActionInfo );
            Assert.IsFalse( IsOn );
        }

        [Test]
        public void TestCase_InitialToggleWithDelayTimeElapsed_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.DelayedToggle( );
            MockedToggeling.Verify( obj => obj.SetTime( new TimeSpan( ) ) );
            MockedToggeling.Verify( obj => obj.Start( ) );
            MockedToggeling.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOn, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.RegularOperation, TestStatus.ActualOperationState );
            Assert.IsTrue( IsOn );
        }

        [Test]
        public void TestCase_HeaterIsExpectingPause_AfterStarted_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );
            TestController.DelayedPause( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsExpectingPause, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.RegularOperation, TestStatus.ActualOperationState );
            Assert.IsTrue( IsOn );
        }

        [Test]
        public void TestCase_HeaterIsDelayedPaused_AfterStarted_StatusCheckWithWiteBoxTest()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );
            TestController.DelayedPause( );

            MockedDelayControllerPause.Verify( obj => obj.SetTime( new TimeSpan( ) ) );
            MockedDelayControllerPause.Verify( obj => obj.Start( ) );

            MockedDelayControllerPause.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );


            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsPaused, TestStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.RegularOperation, TestStatus.ActualOperationState );
            Assert.IsFalse( IsOn );
        }

        [Test]
        public void TestCase_HeaterIsExpectingPause_ButResuming_StatusCheck()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );
            TestController.DelayedPause( );
            TestController.Resume( );

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

    [TestFixture]
    public class HeaterControlPwm_UnitTests
    {
        HeaterControllerPulseWidhtModulation TestController;
        HeaterStatus                         TestStatus;
        Mock<ITimer>                         MockedDelayControllerPause;
        Mock<ITimer>                         MockedToggeling;
        Mock<ITimer>                         MockControlOn;
        Mock<ITimer>                         MockControlLow;
        Mock<ITimer>                         MockControlMiddle;
        Mock<ITimer>                         MockControlHigh;
        Mock<ITimer>                         MockSignal;
        Mock<ITimer>                         MockPwm;
        Mock<ControlTimers>                  MockedHeaterControlTimers;

        void FakeInitialStatusForTesting()
        {
            if (TestStatus != null)
            {
                TestStatus.ActualControllerState = HeaterStatus.ControllerState.InvalidForTesting;
                TestStatus.ActualOperationState = HeaterStatus.OperationState.InvalidForTesting;
                TestStatus.ActualActionInfo = HeaterStatus.InformationAction.InvalidForTesting;
                TestController.SetStatus( TestStatus );
            }
        }

        void MakeAndVerifyControllerTimer( Mock<ITimer> MockedTestTimer )
        {
            MockedTestTimer.Verify( obj => obj.SetTime( new TimeSpan( ) ) );
            MockedTestTimer.Verify( obj => obj.Start( ) );
            MockedTestTimer.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
        }

        void MakeAndVerifyControllerOn( )
        {
            MakeAndVerifyControllerTimer( MockControlOn );
        }

        void MakeAndVerifyControllerPwmLow( )
        {
            MakeAndVerifyControllerOn( );
            MakeAndVerifyControllerTimer( MockControlLow );
        }

        void SetupTest()
        {
            MockedDelayControllerPause                 = new Mock<ITimer>( );
            MockedToggeling                            = new Mock<ITimer>( );
            MockControlOn                              = new Mock<ITimer>( );
            MockControlLow                             = new Mock<ITimer>( );
            MockControlMiddle                          = new Mock<ITimer>( );
            MockControlHigh                            = new Mock<ITimer>( );
            MockSignal                                 = new Mock<ITimer>( );
            MockPwm                                    = new Mock<ITimer>( );
            MockedHeaterControlTimers = new Mock<ControlTimers>( );
            ITimer PauseController                     = MockedDelayControllerPause.Object;
            ITimer TimerForToggeling                   = MockedToggeling.Object;
            ControlTimers HeaterControlTimers          = MockedHeaterControlTimers.Object;
            HeaterControlTimers.TimerOnOff               = MockControlOn.Object;
            HeaterControlTimers.TimerLow               = MockControlLow.Object;
            HeaterControlTimers.TimerMiddle            = MockControlMiddle.Object;
            HeaterControlTimers.TimerHigh              = MockControlHigh.Object;
            HeaterControlTimers.TimerSignal            = MockSignal.Object;
            HeaterControlTimers.TimerPwm               = MockPwm.Object;

            TestController = new HeaterControllerPulseWidhtModulation( new HeaterParameters( ), HeaterControlTimers, PauseController, TimerForToggeling );
            TestStatus = new HeaterStatus( );
        }

        void CleanUpTest()
        {
            MockedDelayControllerPause = null;
            TestController = null;
            TestStatus = null;
        }

        [SetUp]
        public void SetupTests()
        {
            SetupTest( );
        }

        [TearDown]
        public void TearDownTests()
        {
            CleanUpTest( );
        }

        [Test]
        public void TestCase_Pwm_Off_StatusCheck()
        {
            FakeInitialStatusForTesting( );

            bool IsOn = true;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );

            HeaterStatus ReturnedTestedStatus = TestController.GetStatus( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOff, ReturnedTestedStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.Idle, ReturnedTestedStatus.ActualOperationState );
        }

        [Test]
        public void TestCase_PwmOff_To_On_StatusCheck()
        {
            FakeInitialStatusForTesting( );

            bool IsOn = true;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );

            MakeAndVerifyControllerOn( );

            HeaterStatus ReturnedTestedStatus = TestController.GetStatus( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOn, ReturnedTestedStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.RegularOperation, ReturnedTestedStatus.ActualOperationState );
            Assert.AreEqual( HeaterStatus.InformationAction.ItensityChanging, ReturnedTestedStatus.ActualActionInfo );
            Assert.IsTrue( IsOn );
        }

        [Test]
        public void TestCase_Start_To_Confirm_Without_Start_StatusCheck()
        {
            FakeInitialStatusForTesting( );

            bool IsOn = true;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );

            TestController.Confirm( );

            MockControlOn.Verify( obj => obj.Start( ) );

            HeaterStatus ReturnedTestedStatus = TestController.GetStatus( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOff, ReturnedTestedStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.Idle, ReturnedTestedStatus.ActualOperationState );
            Assert.IsFalse( IsOn );
        }

        [Test]
        public void TestCase_PwmOff_To_Low_StatusCheck()
        {
            FakeInitialStatusForTesting( );

            bool IsOn = true;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );

            MakeAndVerifyControllerPwmLow( );

            HeaterStatus ReturnedTestedStatus = TestController.GetStatus( );

            Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOn, ReturnedTestedStatus.ActualControllerState );
            Assert.AreEqual( HeaterStatus.OperationState.PwmIsWorking, ReturnedTestedStatus.ActualOperationState );
            Assert.AreEqual( HeaterStatus.PwmState.Low, ReturnedTestedStatus.ActualPwmState );
            Assert.AreEqual( HeaterStatus.InformationAction.ItensityChanging, ReturnedTestedStatus.ActualActionInfo );
            Assert.IsTrue( IsOn );
        }

        [Test]
        public void TestCase_PwmOff_To_Low_InitialSignalisation()
        {
            FakeInitialStatusForTesting( );

            bool IsOn = true;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );

            MakeAndVerifyControllerPwmLow( );
            MakeAndVerifyControllerTimer( MockSignal );

            HeaterStatus ReturnedTestedStatus = TestController.GetStatus( );

            Assert.IsFalse( IsOn );
        }

        [Test]
        public void TestCase_PwmOff_To_Low_SignalisationSequence()
        {
            FakeInitialStatusForTesting( );

            bool IsOn = true;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );

            MakeAndVerifyControllerPwmLow( );
            MakeAndVerifyControllerTimer( MockSignal );

            for (int i = 1; i <= Settings.SignalCountsForPwmLow; i++)
            {
                MockSignal.Verify( obj => obj.Stop( ), Times.Exactly(i) );
                MockSignal.Verify( obj => obj.Start( ) , Times.Exactly(i+1) );
                MockSignal.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );

                bool EverySecondTime = ( i % 2 ) == 0;

                if ( EverySecondTime )
                {
                    Assert.IsFalse( IsOn );
                }
                else
                {
                    Assert.IsTrue( IsOn );
                }
            }

            HeaterStatus ReturnedTestedStatus = TestController.GetStatus( );

            Assert.AreEqual( 0, ReturnedTestedStatus.ActualSignalisationCounts );
            Assert.IsTrue( IsOn );
        }

        [Test]
        public void TestCase_PwmOff_To_Low_StartWith_HIGH()
        {
            FakeInitialStatusForTesting( );

            bool IsOn = true;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            TestController.Start( );

            MakeAndVerifyControllerPwmLow( );
            MakeAndVerifyControllerTimer( MockPwm ); // PWM HIGH

            HeaterStatus ReturnedTestedStatus = TestController.GetStatus( );

            Assert.IsFalse( IsOn );
        }

    }
}
