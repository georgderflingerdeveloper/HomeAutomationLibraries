using NUnit.Framework;
using TelegrammEvaluator;

namespace TelegrEvaluator_UnitTests
{
    [TestFixture]
    public class WindowTelegrammEvaluator_UnitTests
    {
        WindowTelegrammEvaluator TestWindowEvaluator;

        void Setup()
        {
            TestWindowEvaluator = new WindowTelegrammEvaluator( );
        }

        [Test]
        public void TestEvaluate_AnyWindowIsOpen()
        {
            Setup( );

            WindowState TestState = WindowState.eUnknown;

            TestWindowEvaluator.EInformer += ( sender, e ) =>
            {
                TestState = ( e as EvaluatorEventArgsWindow ).State;
            };

            TestWindowEvaluator.ReceivedTelegramm = VerificationTelegramms.Telegramm_1;

            Assert.AreEqual( WindowState.eAnyWindowIsOpen, TestState );
        }

        [Test]
        public void TestEvaluate_AllWindowsAreClosed()
        {
            Setup( );

            WindowState TestState = WindowState.eUnknown;

            TestWindowEvaluator.EInformer += ( sender, e ) =>
            {
                TestState = ( e as EvaluatorEventArgsWindow ).State;
            };

            TestWindowEvaluator.ReceivedTelegramm = VerificationTelegramms.TelegrammAllClosed;

            Assert.AreEqual( WindowState.eAllWindowsAreClosed, TestState );
        }

        [Test]
        public void TestEvaluate_WindowStateUnknown()
        {
            Setup( );

            WindowState TestState = WindowState.eInvalid;

            TestWindowEvaluator.EInformer += ( sender, e ) =>
            {
                TestState = ( e as EvaluatorEventArgsWindow ).State;
            };

            TestWindowEvaluator.ReceivedTelegramm = VerificationTelegramms.TelegrammUnknown;

            Assert.AreEqual( WindowState.eUnknown, TestState );
        }
    }

    [TestFixture]
    public class DoorTelegrammEvaluator_UnitTests
    {
        DoorTelegrammEvaluator TestDoorEvaluator;

        void Setup()
        {
            TestDoorEvaluator = new DoorTelegrammEvaluator( );
        }

        [Test]
        public void TestEvaluate_AnyDoorIsOpen()
        {
            Setup( );

            DoorState TestState = DoorState.eUnknown;

            TestDoorEvaluator.EInformer += ( sender, e ) =>
            {
                TestState = ( e as EvaluatorEventArgsDoor ).State;
            };

            TestDoorEvaluator.ReceivedTelegramm = VerificationTelegramms.TelegrammDoor;

            Assert.AreEqual( DoorState.eDoorIsOpen, TestState );
        }
    }
}
