using System;
using TelegrammBuilder;

namespace TelegrammEvaluator
{
    public enum WindowState
    {
        eUnknown,
        eOk,
        eAnyWindowIsOpen,
        eAllWindowsAreClosed,
        eInvalid
    }

    public class EvaluatorEventArgsWindow : EventArgs
    {
        public WindowState State { get; set; }
    }
 
    public class WindowTelegrammEvaluator : TelegrammEvaluator
    {
        EvaluatorEventArgsWindow WindowArgs = new EvaluatorEventArgsWindow();

        public override  event Informer EInformer;

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
            if ( telegramm.Contains( TelegrammTypes.WindowInformation ) )
            {
                if ( telegramm.Contains( DeviceStatus.Unknown ) )
                {
                    WindowArgs.State = WindowState.eUnknown;
                    EInformer?.Invoke( this, WindowArgs );
                    return;
                }

                if ( telegramm.Contains( DeviceStatus.Open ) )
                {
                    WindowArgs.State = WindowState.eAnyWindowIsOpen;
                    EInformer?.Invoke( this, WindowArgs );
                    return;
                }

                WindowArgs.State = WindowState.eAllWindowsAreClosed;
                EInformer?.Invoke( this, WindowArgs );
            }
        }
    }
}
