using NUnit.Framework;
using TelegrammBuilder;
using System;
using Moq;
using SystemServices;
using HardConfig.SLEEPINGROOM;
using HardConfig.COMMON;

namespace Telegramm_UnitTests
{
    [TestFixture]
    public class TelegrammBuilder_UnitTests
    {
        Mock<ITimeUtil>       MockTimeStamp;
        MsgTelegrammBuilder   TestBuilder;
        SleepingRoomTelegramm TestTelegramm = new SleepingRoomTelegramm( );
        string[] TestTelgrammElements;
        string Telegramm;

        int WrongIndexForTesting = 0;

        string TestTimeStamp            = "15052017:20h16m33s520ms";
        string DifferentTestTimeStamp   = "19122018:21h29m44s333ms";
        string DifferentTestTimeStamp_2 = "19122018:21h35m44s333ms";
        string VerificationTelegramm_1  = "Schlafzimmer_FENSTERINFORMATION_FensterWestSeite_OFFEN_15052017:20h16m33s520ms_MansardenFensterLinkeSeite_GESCHLOSSEN_19122018:21h29m44s333ms";
        string VerificationTelegramm_2  = "Schlafzimmer_FENSTERINFORMATION_FensterWestSeite_GESCHLOSSEN_19122018:21h29m44s333ms_MansardenFensterLinkeSeite_OFFEN_19122018:21h35m44s333ms";

        int TelegrammIndexForRoom          = 0;
        int TelegrammIndexForTelegrammType = 1;
        int TelegrammIndexForDevice        = 2;
        int TelegrammIndexForStatus        = 3;
        int TelegrammIndexForTimestamp     = 4;

        void SetupMockedTimestamp()
        {
            MockTimeStamp = new Mock<ITimeUtil>( );
            MockTimeStamp.Setup( tobj => tobj.IGetTimeStamp( ) ).Returns( TestTimeStamp );
        }

        void SetupTelegrammBuilder( )
        {
            SetupMockedTimestamp( );
            TestBuilder = new MsgTelegrammBuilder( TestTelegramm , MockTimeStamp.Object );
        }

        void CleanUp()
        {
            TestTelgrammElements = null;
        }

        [Test]
        public void Test_TelegramConfigurationMismatch()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                TestTelgrammElements = e.MsgTelegramm.Split( Seperators.TelegrammSeperator );
            };

            TestBuilder.GotIoChange( WrongIndexForTesting, false );

            Assert.AreEqual( TelegrammStatus.TelegramConfigurationMismatch, TestTelgrammElements[TelegrammIndexForRoom] );
        }

        [Test]
        public void Test_Telegramm_Element_Type()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                TestTelgrammElements = e.MsgTelegramm.Split( Seperators.TelegrammSeperator );
            };

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, false );

            Assert.AreEqual( TelegrammTypes.WindowInformation, TestTelgrammElements[TelegrammIndexForTelegrammType]);
        }

        [Test]
        public void Test_Telegramm_Element_RoomIdentification()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                TestTelgrammElements = e.MsgTelegramm.Split( Seperators.TelegrammSeperator );
            };

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, false );

            Assert.AreEqual( SleepingRoomDeviceNames.RoomName, TestTelgrammElements[TelegrammIndexForRoom] );
        }

        [Test]
        public void Test_NextTelegramm_Element_DeviceType()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                TestTelgrammElements = e.MsgTelegramm.Split( Seperators.TelegrammSeperator );
            };

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, false );

            Assert.AreEqual( SleepingRoomDeviceNames.WindowWest_, TestTelgrammElements[TelegrammIndexForDevice] );
        }

        [Test]
        public void Test_NextTelegramm_Element_TimeStamp()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                TestTelgrammElements = e.MsgTelegramm.Split( Seperators.TelegrammSeperator );
            };

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, false );

            Assert.AreEqual( TestTimeStamp, TestTelgrammElements[TelegrammIndexForTimestamp] );
        }

        [Test]
        public void Test_NextTelegramm_Element_StatusIsClosed()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                TestTelgrammElements = e.MsgTelegramm.Split( Seperators.TelegrammSeperator );
            };

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, false );

            Assert.AreEqual( DeviceStatus.Closed, TestTelgrammElements[TelegrammIndexForStatus] );
        }

        [Test]
        public void Test_NextTelegramm_Element_StatusIsOpen()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                TestTelgrammElements = e.MsgTelegramm.Split( Seperators.TelegrammSeperator );
            };

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, true  );

            Assert.AreEqual( DeviceStatus.Open, TestTelgrammElements[TelegrammIndexForStatus] );
        }

        [Test]
        public void Test_TelegrammGeneration()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                Telegramm = e.MsgTelegramm;
            };

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, true );
            MockTimeStamp.Setup( tobj => tobj.IGetTimeStamp( ) ).Returns( DifferentTestTimeStamp );
            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputMansardWindowNorthLeft, false );

            Assert.AreEqual( VerificationTelegramm_1, Telegramm );
        }

        [Test]
        public void Test_UpdateDiagramm_Window_Closed_Open_()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                Telegramm = e.MsgTelegramm;
            };

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, true );
            MockTimeStamp.Setup( tobj => tobj.IGetTimeStamp( ) ).Returns( DifferentTestTimeStamp );
            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputMansardWindowNorthLeft, false );

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, false );
            MockTimeStamp.Setup( tobj => tobj.IGetTimeStamp( ) ).Returns( DifferentTestTimeStamp_2 );
            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputMansardWindowNorthLeft, true );

            Assert.AreEqual( VerificationTelegramm_2, Telegramm );
        }

    }
}
