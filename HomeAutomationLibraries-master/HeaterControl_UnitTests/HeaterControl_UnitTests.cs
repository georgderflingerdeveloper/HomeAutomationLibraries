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
        }

        [Test]
        public void TestCase_HeaterIsOff_StatusCheck()
        {
           SetupTest( );
           FakeInitialStatusForTesting( );

           TestController.EActivityChanged += ( sender, e ) =>
           {
                TestStatus = e.Status;
           };

           TestController.Stop( );

           Assert.AreEqual( HeaterStatus.ControllerState.ControllerIsOff, TestStatus.ActualControllerState );
           Assert.AreEqual( HeaterStatus.OperationState.Idle, TestStatus.ActualOperationState );
        }

        //[Test]
        //public void TestCase_StartHeaterDirectly_StatusCheck()
        //{
        //}

    }


}
