using NUnit.Framework;
using TimerMockable;
using Moq;
using System;
using System.Timers;
using Power_Controller;
using UnivCommonController;

namespace PowerController_UnitTest
{
    [TestFixture]
    public class PowerController_UnitTests
    {
        ControlTimers   TestTimers;
        Mock<ITimer>    MockedTimerAutomaticOff;
        PowerStatus     TestPowerStatus;
        PowerEventArgs  TestPowerEventArgs;
        PowerController TestPowerController;
        PowerParameters TestPowerParameters;

        void RaiseTimer( Mock<ITimer> TimerToRaise)
        {
            TimerToRaise.Raise(obj => obj.Elapsed += null, new EventArgs() as ElapsedEventArgs);
        }

        void SetupTest()
        {
            MockedTimerAutomaticOff = new Mock<ITimer>();
            TestTimers = new ControlTimers()
            {
                TimerAutomaticOff = MockedTimerAutomaticOff.Object
            };

            TestPowerStatus     = new PowerStatus();
            TestPowerEventArgs  = new PowerEventArgs();
            TestPowerParameters = new PowerParameters();
            TestPowerController = new PowerController(TestPowerParameters, TestTimers); 
        }

        [SetUp]
        public void SetupTests()
        {
            SetupTest();
        }

        [Test]
        public void TestCase_PowerStart()
        {
            bool IsOn = false;
            
            TestPowerController.EActivityChanged += (sender, e) =>
            {
                IsOn = e.TurnOn;
            };

            TestPowerController.Start();

            Assert.IsTrue(IsOn);

        }

        [Test]
        public void TestCase_PowerStart_Status()
        {
            TestPowerStatus.ActualOperationState = PowerStatus.OperationState.InvalidForTesting;
            TestPowerStatus.ActualControllerState = ControllerInformer.ControllerState.InvalidForTesting;

            TestPowerController.EActivityChanged += (sender, e) =>
            {
                TestPowerStatus = e.Status;
            };

            TestPowerController.Start();

            Assert.AreEqual(ControllerInformer.ControllerState.ControllerIsOn, TestPowerStatus.ActualControllerState);
            Assert.AreEqual(PowerStatus.OperationState.TimerActive, TestPowerStatus.ActualOperationState);
        }

        [Test]
        public void TestCase_PowerStart_Timer()
        {
           TestPowerController.Start();

           MockedTimerAutomaticOff.Verify(obj => obj.SetTime(TestPowerParameters.DurationPowerOn));
           MockedTimerAutomaticOff.Verify(obj => obj.Stop(), Times.AtLeastOnce());
           MockedTimerAutomaticOff.Verify(obj => obj.Start(), Times.AtLeastOnce());
        }

        [Test]
        public void TestCase_PowerStart_TimerElapsed()
        {
            bool IsOn = true;

            TestPowerController.EActivityChanged += (sender, e) =>
            {
                IsOn = e.TurnOn;
            };

            RaiseTimer(MockedTimerAutomaticOff);

            Assert.IsFalse(IsOn);
        }

        [Test]
        public void TestCase_PowerStart_TimerElapsedTurItOff()
        {
            RaiseTimer(MockedTimerAutomaticOff);
            MockedTimerAutomaticOff.Verify(obj => obj.Stop(), Times.AtLeastOnce());
        }

        [Test]
        public void TestCase_PowerStart_TimerFinished()
        {
            TestPowerStatus.ActualOperationState = PowerStatus.OperationState.InvalidForTesting;
            TestPowerStatus.ActualControllerState = ControllerInformer.ControllerState.InvalidForTesting;

            TestPowerController.EActivityChanged += (sender, e) =>
            {
                TestPowerStatus = e.Status;
            };

            RaiseTimer(MockedTimerAutomaticOff);

            Assert.AreEqual(ControllerInformer.ControllerState.ControllerIsOff, TestPowerStatus.ActualControllerState);
            Assert.AreEqual(PowerStatus.OperationState.TimerFinished, TestPowerStatus.ActualOperationState);
        }

        [Test]
        public void TestCase_Power_Stop()
        {
            bool IsOn = true;

            TestPowerController.EActivityChanged += (sender, e) =>
            {
                IsOn = e.TurnOn;
            };

            TestPowerController.Start();
            TestPowerController.Stop();

            Assert.IsFalse(IsOn);

        }

        [Test]
        public void TestCase_Power_Stop_States()
        {
            TestPowerStatus.ActualOperationState = PowerStatus.OperationState.InvalidForTesting;
            TestPowerStatus.ActualControllerState = ControllerInformer.ControllerState.InvalidForTesting;

            TestPowerController.EActivityChanged += (sender, e) =>
            {
                TestPowerStatus = e.Status;
            };


            TestPowerController.Start();
            TestPowerController.Stop();

            Assert.AreEqual(ControllerInformer.ControllerState.ControllerIsOff, TestPowerStatus.ActualControllerState);
            Assert.AreEqual(PowerStatus.OperationState.TimerInterrupted, TestPowerStatus.ActualOperationState);
        }


        [TearDown]
        public void TearDown()
        {
            MockedTimerAutomaticOff = null;
            TestTimers              = null;
            TestPowerStatus         = null;
            TestPowerEventArgs      = null;
        }

    }

}
