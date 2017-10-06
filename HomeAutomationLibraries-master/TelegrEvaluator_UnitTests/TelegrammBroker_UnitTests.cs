using NUnit.Framework;
using System;
using TelegrammEvaluator;

namespace TelegrEvaluator_UnitTests
{
    [TestFixture]
    public class TelegrammBroker_UnitTests
    {
        TelegrammBrocker TestBroker;
        object ExpectedObject;

        void Setup( )
        {
            TestBroker = new TelegrammBrocker( new EvaluatorCollection() );
        }

        [Test]
        public void TestBrockerOnWindowMessage()
        {
            Setup();
            WindowState TestState = WindowState.eUnknown;
            EvaluatorEventArgsWindow WindowArgs;

            TestBroker.BEInformer += ( sender, e ) => 
            {
                ExpectedObject = sender;

                if( ExpectedObject is WindowTelegrammEvaluator )
                {
                    WindowArgs = e as EvaluatorEventArgsWindow;
                    TestState = WindowArgs.State;
                }
            };

            TestBroker.ReceivedTelegramm = VerificationTelegramms.Telegramm_1;

            Assert.AreEqual( WindowState.eAnyWindowIsOpen, TestState );
        }

        [Test]
        public void TestBrockerOnDoorMessage()
        {
            Setup();
            DoorState TestState = DoorState.eUnknown;
            EvaluatorEventArgsDoor DoorArgs;

            TestBroker.BEInformer += ( sender, e ) => 
            {
                ExpectedObject = sender;

                if( ExpectedObject is DoorTelegrammEvaluator )
                {
                    DoorArgs = e as EvaluatorEventArgsDoor;
                    TestState = DoorArgs.State;
                }
            };

            TestBroker.ReceivedTelegramm = VerificationTelegramms.TelegrammDoor;

            Assert.AreEqual( DoorState.eDoorIsOpen, TestState );
        }

    }
}
