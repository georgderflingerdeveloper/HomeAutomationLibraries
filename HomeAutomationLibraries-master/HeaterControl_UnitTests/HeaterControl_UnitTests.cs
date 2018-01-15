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

            MockedToggeling.Verify( obj => obj.SetTime( new HeaterParameters().CmdDurationForTurningStartingStopping ) );
            MockedToggeling.Verify( obj => obj.Start( ) );
            MockedToggeling.Verify( obj => obj.Stop( ) );
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

            MockedDelayControllerPause.Verify( obj => obj.SetTime( new HeaterParameters().DurationDelayPause ));
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
        Mock<ITimer>                         MockPause;
        Mock<ITimer>                         MockDelayedToggeling;
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

        void MakeAndVerifyControllerTimer( Mock<ITimer> MockedTestTimer, TimeSpan ConfiguredTime )
        {
            MockedTestTimer.Verify( obj => obj.SetTime( ConfiguredTime ) );
            MockedTestTimer.Verify( obj => obj.Start( ) );
            MockedTestTimer.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
        }

        void PwmStartedAndElapsed()
        {
            MockPwm.Verify( obj => obj.Start( ) );
            MockPwm.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
        }

        void MakeAndVerifyPwmLowOff()
        {
            MockPwm.Verify( obj => obj.SetTime( new HeaterParameters().SignalDurationLowOff ));
            PwmStartedAndElapsed( );
        }

        void MakeAndVerifyPwmLowOn()
        {
            MockPwm.Verify( obj => obj.Start( ) );
            MockPwm.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
        }

        void MakeAndVerifyControllerOn( )
        {
            MakeAndVerifyControllerTimer( MockControlOn, new HeaterParameters().CmdDurationTurningOn );
        }

        void MakeAndVerifyControllerPwmLow( )
        {
            MakeAndVerifyControllerOn( );
            MakeAndVerifyControllerTimer( MockControlLow, new HeaterParameters( ).SignalDurationLowOn );
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
            MockPause                                  = new Mock<ITimer>( );
            MockDelayedToggeling                       = new Mock<ITimer>( );
            MockedHeaterControlTimers                  = new Mock<ControlTimers>( );
            ControlTimers HeaterControlTimers          = MockedHeaterControlTimers.Object;
            HeaterControlTimers.TimerOn                = MockControlOn.Object;
            HeaterControlTimers.TimerLow               = MockControlLow.Object;
            HeaterControlTimers.TimerMiddle            = MockControlMiddle.Object;
            HeaterControlTimers.TimerHigh              = MockControlHigh.Object;
            HeaterControlTimers.TimerSignal            = MockSignal.Object;
            HeaterControlTimers.TimerPwm               = MockPwm.Object;
            HeaterControlTimers.TimerPause             = MockPause.Object;
            HeaterControlTimers.TimerToggelingDelay    = MockDelayedToggeling.Object;

            TestController = new HeaterControllerPulseWidhtModulation( new HeaterParameters( ), HeaterControlTimers );
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
            MakeAndVerifyControllerTimer( MockSignal, new HeaterParameters( ).SignalDurationSignalisation );

            HeaterStatus ReturnedTestedStatus = TestController.GetStatus( );

            Assert.IsFalse( IsOn );
        }

        [Test]
        public void TestCase_PwmOff_To_Low_SignalisationStart()
        {
            FakeInitialStatusForTesting( );

            bool IsOn = true;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };


            MockControlLow.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
            MockSignal.Verify( obj => obj.Start( ) );
        }

        [Test]
        public void TestCase_PwmOff_To_Low_SignalisationSequence()
        {
            FakeInitialStatusForTesting( );

            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            for (int i = 1; i <= Settings.SignalCountsForPwmLow; i++)
            {
                MockSignal.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
                MockSignal.Verify( obj => obj.Stop( ), Times.Exactly( i ) );
                MockSignal.Verify( obj => obj.Start( ), Times.Exactly( i ) );

                bool EverySecondTime = ( i % 2 ) == 0;

                if (EverySecondTime)
                {
                    Assert.IsTrue( IsOn );
                }
                else
                {
                    Assert.IsFalse( IsOn );
                }
            }
        }

        [Test]
        public void TestCase_PwmOff_To_Low()
        {
            MockControlLow.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );

            MockPwm.Verify( obj => obj.Start( ) );
            MockPwm.Verify( obj => obj.SetTime( new HeaterParameters().SignalDurationLowOn ) );
        }

        [Test]
        public void TestCase_PwmStartedWithLowParametersIsNowLOW()
        {
            MockControlLow.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
            MockPwm.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );

            MockPwm.Verify( obj => obj.Stop( ) );
            MockPwm.Verify( obj => obj.SetTime( new HeaterParameters( ).SignalDurationLowOff ) );
            MockPwm.Verify( obj => obj.Start( ), Times.Exactly( 2 ) );
        }

        [Test]
        public void TestCase_PwmStartedWithLowParametersIsNowLOW_CheckIfTurnedOff()
        {
            bool IsOn = true;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            MockControlLow.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
            MockPwm.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );

            Assert.IsFalse( IsOn );
        }

        [Test]
        public void TestCase_PwmStartedWithLowParametersIsNowHigh()
        {
            MockControlLow.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
            MockPwm.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
            MockPwm.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );

            // first time is creation
            // second time is pwm low
            // third time is pwm on again
            MockPwm.Verify( obj => obj.SetTime( new HeaterParameters( ).SignalDurationLowOn ), Times.Exactly( 3 ) );
        }

        [Test]
        public void TestCase_PwmStartedWithLowParametersIsNowHIGH_CheckIfTurnedOn()
        {
            bool IsOn = false;

            TestController.EActivityChanged += ( sender, e ) =>
            {
                TestStatus = e.Status;
                IsOn = e.TurnOn;
            };

            MockControlLow.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
            MockPwm.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
            // check alternating turn on/off
            Assert.IsFalse( IsOn );
            MockPwm.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
            Assert.IsTrue( IsOn );
            MockPwm.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
            Assert.IsFalse( IsOn );
            MockPwm.Raise( obj => obj.Elapsed += null, new EventArgs( ) as ElapsedEventArgs );
            Assert.IsTrue( IsOn );
        }

        [Test]
        public void TestControllerIsInitialised()
        {
             HeaterControllerPulseWidhtModulation Test_Controller = new HeaterControllerPulseWidhtModulation( new HeaterParameters(), new InialisedTimers().Timers );
        }

    }
}
