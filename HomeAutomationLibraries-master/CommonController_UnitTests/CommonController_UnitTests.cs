using NUnit.Framework;
using UnivCommonController;
using TimerMockable;
using Moq;
using System;
using System.Timers;

namespace CommonController_UnitTests
{

    [TestFixture]
    public class CommonController_UnitTests
    {
        CommonController    TestController;
        ControllerInformer TestInformer;

        [SetUp]
        public void SetupTests()
        {
            TestController = new CommonController();
            TestInformer = new ControllerInformer();
        }

        [Test]
        public void Test_Start()
        {
            bool IsOn = false;
            TestController.EActivityChanged += (sender, e) =>
            {
                IsOn = e.TurnOn;
                TestInformer = e.Informer;
            };

            TestController.Start();

            Assert.IsTrue(IsOn);
        }

        [Test]
        public void Test_Start_Informer()
        {
            TestController.EActivityChanged += (sender, e) =>
            {
                TestInformer = e.Informer;
            };

            TestController.Start();

            Assert.AreEqual(ControllerInformer.ControllerState.ControllerIsOn, TestInformer.ActualControllerState);
        }

        [Test]
        public void Test_Start_Stop()
        {
            bool IsOn = true;
            TestController.EActivityChanged += (sender, e) =>
            {
                IsOn = e.TurnOn;
                TestInformer = e.Informer;
            };

            TestController.Start();
            TestController.Stop();

            Assert.IsFalse(IsOn);
        }

        [Test]
        public void Test_Stop_Informer()
        {
            TestController.EActivityChanged += (sender, e) =>
            {
                TestInformer = e.Informer;
            };

            TestController.Start();
            TestController.Stop();

            Assert.AreEqual(ControllerInformer.ControllerState.ControllerIsOff, TestInformer.ActualControllerState);
        }

        [TearDown]
        public void TearDownTests()
        {
            TestController = null;
        }
    }
}
