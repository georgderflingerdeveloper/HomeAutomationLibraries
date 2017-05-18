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
        int WrongIndexForTesting = 0;

        string TestTimeStamp = "15052017:20h16m33s520ms";

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

            Assert.AreEqual( TelegrammStatus.TelegramConfigurationMismatch, TestTelgrammElements[0] );
        }

        [Test]
        public void Test_FirstTelegramm_Element_RoomIdentification()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                TestTelgrammElements = e.MsgTelegramm.Split( Seperators.TelegrammSeperator );
            };

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, false );

            Assert.AreEqual( SleepingRoomDeviceNames.RoomName, TestTelgrammElements[0] );
        }

        [Test]
        public void Test_SecondTelegramm_Element_Timestamp()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                TestTelgrammElements = e.MsgTelegramm.Split( Seperators.TelegrammSeperator );
            };

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, false );

            Assert.AreEqual( TestTimeStamp, TestTelgrammElements[1] );
        }

        [Test]
        public void Test_NextTelegramm_Element_FurnitureType()
        {
            CleanUp( );

            SetupTelegrammBuilder( );

            TestBuilder.ENewTelegramm += ( sender, e ) =>
            {
                TestTelgrammElements = e.MsgTelegramm.Split( Seperators.TelegrammSeperator );
            };

            TestBuilder.GotIoChange( SleepingRoomIODeviceIndices.indDigitalInputWindowWest, false );

            Assert.AreEqual( SleepingRoomDeviceNames.WindowWest_, TestTelgrammElements[2] );
        }


    }
}
