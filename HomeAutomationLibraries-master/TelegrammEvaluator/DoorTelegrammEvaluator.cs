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
        public delegate void Informer( object sender, EvaluatorEventArgsDoor e );
        EvaluatorEventArgsDoor DoorArgs = new EvaluatorEventArgsDoor();
        public new event Informer EInformer;

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
