using NUnit.Framework;
using System;
using TelegrammEvaluator;
namespace TelegrammEvaluator_UnitTests
{
    [TestFixture]
    public class WindowTelegrammEvaluator_UnitTests
    {
        WindowTelegrammEvaluator TestWindowEvaluator;
        string VerificationTelegramm_1         = "Schlafzimmer_FENSTERINFORMATION_FensterWestSeite_OFFEN_15052017:20h16m33s520ms_MansardenFensterLinkeSeite_GESCHLOSSEN_19122018:21h29m44s333ms";
        string VerificationTelegramm_2         = "Schlafzimmer_FENSTERINFORMATION_FensterWestSeite_GESCHLOSSEN_19122018:21h29m44s333ms_MansardenFensterLinkeSeite_OFFEN_19122018:21h35m44s333ms";
        string VerificationTelegrammAllClosed  = "Schlafzimmer_FENSTERINFORMATION_FensterWestSeite_GESCHLOSSEN_19122018:21h29m44s333ms_MansardenFensterLinkeSeite_GESCHLOSSEN_19122018:21h35m44s333ms";
        string VerificationTelegrammUnknown    = "Schlafzimmer_FENSTERINFORMATION_FensterWestSeite_UNBEKANNT_19122018:21h29m44s333ms_MansardenFensterLinkeSeite_OFFEN_19122018:21h35m44s333ms";

        void Setup( )
        {
            TestWindowEvaluator = new WindowTelegrammEvaluator();
        }

        [Test]
        public void TestEvaluate_AnyWindowIsOpen( )
        {
            Setup();

            WindowState TestState = WindowState.eUnknown;

            TestWindowEvaluator.EInformer += (sender, e) => 
            {
                TestState = e.State;
            };

            TestWindowEvaluator.ReceivedTelegramm = VerificationTelegramm_1;

            Assert.AreEqual( WindowState.eAnyWindowIsOpen, TestState );

        }

        [Test]
        public void TestEvaluate_AllWindowsAreClosed( )
        {
            Setup();

            WindowState TestState = WindowState.eUnknown;

            TestWindowEvaluator.EInformer += (sender, e) => 
            {
                TestState = e.State;
            };

            TestWindowEvaluator.ReceivedTelegramm = VerificationTelegrammAllClosed;

            Assert.AreEqual( WindowState.eAllWindowsAreClosed, TestState );

        }

        [Test]
        public void TestEvaluate_WindowStateUnknown( )
        {
            Setup();

            WindowState TestState = WindowState.eInvalid;

            TestWindowEvaluator.EInformer += (sender, e) => 
            {
                TestState = e.State;
            };

            TestWindowEvaluator.ReceivedTelegramm = VerificationTelegrammUnknown;

            Assert.AreEqual( WindowState.eUnknown, TestState );

        }

    }
}
