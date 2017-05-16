using System;

namespace HardConfig
{
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
        //  public static readonly string RequestForClientConnection = IPConfiguration.Prefix.TCPCLIENT + "ConnectToMe";
        public const string PressAnyKeyToTerminateApplication = "Press any key to terminate application ...";
        public const string PressEnterForTerminateApplication = "Press enter for terminate application";
        public const string Terminated = "Terminated";
        public const string DeviceDigitalInput = "Device digital input ";
        public const string DeviceDigialOutput = "Device digital output";
        public const string BraceOpen = "(";
        public const string BraceClose = ")";
    }
}
