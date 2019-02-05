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
