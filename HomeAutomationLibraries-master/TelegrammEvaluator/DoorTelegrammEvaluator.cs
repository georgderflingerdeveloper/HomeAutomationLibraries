using System;
using TelegrammBuilder;

namespace TelegrammEvaluator
{
    public enum DoorState
    {
        eUnknown,
        eOk,
        eDoorIsOpen
    }

    public class EvaluatorEventArgsDoor : EventArgs
    {
        public DoorState State { get; set; }
    }

    public class DoorTelegrammEvaluator : TelegrammEvaluator
    {
        EvaluatorEventArgsDoor DoorArgs = new EvaluatorEventArgsDoor();
        public override event Informer EInformer;

        public override string ReceivedTelegramm
        {
            set
            {
                _receivedTelegramm = value;
                Evaluate( _receivedTelegramm );
            }
        }

        void Evaluate( string telegramm )
        {
            // Test 
            EInformer?.Invoke( this, DoorArgs );
        }
    }
}
