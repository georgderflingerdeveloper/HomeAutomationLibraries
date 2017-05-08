using System;
namespace LibUdp.BASIC.CONSTANTS
{
    public static class BasicErrorMessage
    {
        public const string InvalidIpAdress   = "Invalid Ip adress";
        public const string InvalidPortNumber = "Invalid port number";
        public const string DataSendingFailed = "Data sending failed";
        public const string InvalidSendString = "Invalid send string";
        public const string ReceiveDataFailed = "Receive of data failed";
    }

    public static class BasicIpSettings
    {
        public const string Localhost = "127.0.0.1";
        public const int DefaultPort = 5000;
    }
}
