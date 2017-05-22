using System;
using TelegrammEvaluator.INTERFACE;
using HardConfig.COMMON;

namespace TelegrammEvaluator
{
    public enum EvaluatorState
    {
        eUnknown,
        eOk,
        eAlarmWindowIsOpen,
        eAlarmDoorIsOpen
    }

    public class EvaluatorEvents : EventArgs
    {
        public EvaluatorState State { get; set; }
    }

    public delegate void Informer( object sender, EvaluatorEvents e );

    public class TelegrammEvaluator : ITelegrammEvaluator
    {
        EvaluatorState State;
        string _receivedTelegramm;

        public TelegrammEvaluator( )
        {
        }





        public string ReceivedTelegramm { get => _receivedTelegramm; set => _receivedTelegramm = value; }
        public event Informer EInformer;

    }
}
