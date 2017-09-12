using System;
using TelegrammBuilder;

namespace TelegrammEvaluator
{
    public enum DoorState
    {
        eUnknown,
        eOk,
        eDoorIsOpen,
        eDoorIsClosed
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
            if ( telegramm.Contains( TelegrammTypes.DoorInformation ) )
            {
                if ( telegramm.Contains( DeviceStatus.Unknown ) )
                {
                    DoorArgs.State = DoorState.eUnknown;
                    EInformer?.Invoke( this, DoorArgs );
                    return;
                }

                if ( telegramm.Contains( DeviceStatus.Open ) )
                {
                    DoorArgs.State = DoorState.eDoorIsOpen;
                    EInformer?.Invoke( this, DoorArgs );
                    return;
                }

                DoorArgs.State = DoorState.eDoorIsClosed;
                EInformer?.Invoke( this, DoorArgs );
            }
        }
    }
}
