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
            bool IsOn = false;
            TestController.EActivityChanged += (sender, e) =>
            {
                IsOn = e.TurnOn;
                TestInformer = e.Informer;
            };

            TestController.Start();

            Assert.AreEqual(ControllerInformer.ControllerState.ControllerIsOn, TestInformer.ActualControllerState);
        }

        [TearDown]
        public void TearDownTests()
        {
            TestController = null;
        }
    }
}
