using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardConfig;
using HardConfig.COMMON;

namespace HardConfig
{
    static class HADictionaries
    {
        public static Dictionary<string, int> DeviceDictionaryCenterdigitalOut = new Dictionary<string, int>
        {
 //TODO         {HardwareDevices.HeaterNursery,                                  SleepingRoomIODeviceIndices.indDigitalOutputHeater                                         },
                {InfoString.DeviceNotFound,                                      GeneralConstants.DeviceNotFound                                                            },
        };

        public static Dictionary<string, int> DeviceDictionaryCenterdigitalIn = new Dictionary<string, int>
        {
 // TODO        {HardwareDevices.HeaterDryerBathRoom,                            BathRoomIODeviceIndices.indDigitalOutputBathRoomHeater                                     },
                {InfoString.DeviceNotFound,                                      GeneralConstants.DeviceNotFound                                                            },
        };

        public static Dictionary<string, int> DeviceDictionaryAnteRoomdigitalOut = new Dictionary<string, int>
        {
                {InfoString.DeviceNotFound,                                      GeneralConstants.DeviceNotFound                                                            },
        };

        public static Dictionary<string, int> DeviceDictionaryAnteroomdigitalIn = new Dictionary<string, int>
        {
                {InfoString.DeviceNotFound,                                      GeneralConstants.DeviceNotFound                                                            },
        };


        public static string GetKeyByValue( ref Dictionary<string, int> dic, int value )
        {
            foreach (KeyValuePair<string, int> pair in dic)
            {
                if (pair.Value == value)
                {
                    return pair.Key;
                }
            }
            return InfoString.DeviceNotFound;
        }
    }

    static class EscapeSequences
    {
        public const string CRLF = "\r\n";
        public const string CR = "\r";
    }

    static class DeviceCathegory
    {
        public const string Door = "Door";
        public const string Window = "Window";
        public const string Mansard = "Mansard";
        public const string Rainsensor = "Rainsensor";
    }

    static class InfoObjectDefinitions
    {
        public const string Room = "ROOM";
        public const string Server = "SERVER";
        public const string Port = "PORT";
        public const string HeatersLivingRoomAutomaticOnOff = "HEATERSLIVINGROOMAUTO";
    }

    static class Seperators
    {
        public static char[] delimiterChars = { ' ', ',', '.', ':', '\t', '#' };
        public static char[] delimiterCharsExtended = { ' ', ',', '.', ':', '\t', '#', '_' };
        public static char[] delimiterCharsSchedulerReserved = { ' ', '_' };
        public const string WhiteSpace = " ";
        public const string InfoSeperator = "_";
    }

    static class GeneralConstants
    {
        public const double TimerDisabled = 0;
        public const int NumberOfOutputsIOCard = 16;
        public const int NumberOfInputsIOCard = 16;
        public const int DeviceNotFound = -1;
        public const string DigitalInput = "DigitalInput:";
        public const string DigitalOutput = "DigitalOutput:";
        // for some string operations : is intepreted as delimiter - so we don´t need this in certain cases!
        public const string DigitalInput_ = "DigitalInput";
        public const string DigitalOutput_ = "DigitalOutput";
        public const bool ON = true;
        public const bool OFF = false;
        public const bool STARTAUTOMATICOFF = false;
        public const bool STOPAUTOMATICOFF = false;
        public const double DURATION_COMMONTICK = 1000.0;
        public const string SlashUsedInLinux = "//";
        public const string BackSlash = "\\";
    }

    static class FormatConstants
    {
        public const string TransactionNumberFormat = "{0:000000000}";
        public const string TransactionNumberFormat_ = "000000000";
    }

    public static class InfoString
    {
        public const string InfoPhidgetException = "Phidget Exception";
        public const string InfoNoIO = "No IO primer is attached ( probably unplugged )";
        public const string AppPrefix = "Home Automation Comander: ";
        public const string AppCmdLstPrefix = "Home Automation Comado List: ";
        public const string ConfigFileName = "conf.ini";
        public const string IniSection = "SECTION";
        public const string IniSectionPhidgets = "PHIDGETS";
        public const string OperationMode = "Selected Operation Mode: ";
        public const string DeviceNotFound = "Device not found!";
        public const string FailedToEstablishClient = "Failed to establish client! ";
        public const string FailedToEstablishUDPBroadcast = "Failed to establish UDP invitation Broadcast! ";
        public const string FailedToEstablishUDPReceive = "Failed to establisch UDP receive!";
        public const string FailedToEstablishUDPSend = "Failed to establisch UDP send!";
        public const string FailedToEstablishServer = "Failed to establish server! ";
        public const string ReceiveInvitationNotPossible = "Receive invitation data is not possible! reason:";
        public const string GreetingToServer = "Hello Server - This is ";
        public const string MyComputernameIs = "  my computername is";
        public const string SorryServerIsNotConnectedWithClient = "Sorry server is not connected with client!";
        public const string ComunicationProtocollError = "Home automation comunication protocoll Error!";
        public const string SchedulerIsStartingDevice = "Scheduler is Starting Device: ";
        public const string SchedulerIsStopingDevice = "Scheduler is Stopping Device: ";
        public const string SchedulerIsRescheduling = "Scheduler is rescheduling: ";
        public const string Asking = "Is asking ...";
        public const string Scheduler = "Scheduler";
        public const string StatusOf = "Status of ";
        public const string Is = "is:";
        public const string FailedToRecoverSchedulerData = "Failed to recover scheduler data!";
        public const string LocationOutside = "Outside";
        public const string InfoConnectionFailed = "Sorry - connection failed - press enter for reconnect";
        public const string InfoTryToReconnect = "Try to reconnect ...";
        public const string InfoStartingTime = "Starting time: ";
        public const string InfoVersion = "Version: ";
        public const string InfoLastBuild = "Last build: ";
        public const string InfoVersioninformation = "Versioninformation: ";
        public const string InfoLoadingConfiguration = "Loading configuration ...";
        public const string InfoExpectingPhidget = "Expecting Phidget ";
        public const string InfoNoConfiguredPhidgetIDused = "No configured Phidget ID´s used...";
        public const string InfoLoadingConfigurationSucessfull = "Loading configuration successfull";
        public const string InfoIniDidNotFindProperConfiguration = "Did not find any proper configuration! - check content of ini file";
        public const string FailedToLoadConfiguration = "Failed to load configuration!";
        public const string InfoTypeExit = "press (E) or (enter) ... for leave";
        public const string Exit = "EXIT";
        public const string StartTimerForRecoverScheduler = "Start timer for recover scheduler ...";
        public const string RemainingTime = "Remaining time ... ";
        public const string PressAnyKeyToTerminateApplication = "Press any key to terminate application ...";
        public const string PressEnterForTerminateApplication = "Press enter for terminate application";
        public const string Terminated = "Terminated";
        public const string DeviceDigitalInput = "Device digital input ";
        public const string DeviceDigialOutput = "Device digital output";
        public const string BraceOpen = "(";
        public const string BraceClose = ")";
        public const string On = "ON";
        public const string Off = "OFF";
    }

    static class InfoOperationMode
    {
        public const string SLEEPING_ROOM = "SLEEPINGROOM";
        public const string CENTER_KITCHEN_AND_LIVING_ROOM = "CENTERKITCHENLIVINGROOM";
        public const string ANTEROOM = "ANTEROOM";
        public const string LIVING_ROOM_EAST = "LIVINGROOMEAST";
        public const string LIVING_ROOM_WEST = "LIVINGROOMWEST";
        public const string OUTSIDE = "OUTSIDE";
        public const string IO_CTRL = "IO_CTRL";
        public const string TEST_PWM = "TEST_PWM";
        public const string ANALOG_HEATER = "ANALOG_HEATER";
        public const string LED_CTRL = "LED_CTRL";
        public const string TCP_COMUNICATION_SERVER = "TCP_SERVER";
        public const string TCP_COMUNICATION_CLIENT = "TCP_CLIENT";
        public const string TCP_COMUNICATION_CLIENT_SINGLE_MESSAGES = "TCP_CLIENT_SINGLE_MESSAGES";
        public const string TCP_COMUNICATION_CLIENT_INVITE = "TCP_CLIENT_CONNECT_AFTER_INVITATION";
        public const string TCP_COMUNICATION_CLIENT_SEND_PERIODIC = "TCP_CLIENT_SEND_PERIODIC";
        public const string TCP_COMUNICATION_CLIENT_INVITE_LOCALHOST = "TCP_CLIENT_CONNECT_AFTER_INVITATION_LOCALHOST";
        public const string TCP_COMUNICATION_CLIENT_INTERNAL = "TCP_CLIENT_INTERNAL";
        public const string TCP_COMUNICATION_CLIENT_STRESSTEST = "TCP_CLIENT_STRESSTEST";
        public const string TCP_COMUNICATION_SERVER_INTERNAL = "TCP_SERVER_INTERNAL";
        public const string TCP_COMUNICATION_SERVER_TEST_GET_MESSAGES_FROM_CLIENTS = "TCP_SERVER_SHOW_CLIENT_MSG";
        public const string TCP_COMUNICATION_SERVER_SEND_PERIODIC = "TCP_SERVER_SEND_PERIODIC";
        public const string TCP_COMUNICATION_SERVER_SEND_PERIODIC_LOCALHOST = "TCP_SERVER_SEND_PERIODIC_LOCALHOST";
        public const string TCP_COMUNICATION_SERVER_INVITE = "TCP_SERVER_INVITE_CLIENTS";
    }

}
