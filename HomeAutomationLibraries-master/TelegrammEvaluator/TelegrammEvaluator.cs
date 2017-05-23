using System;
using TelegrammEvaluator.INTERFACE;
using HardConfig.COMMON;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TelegrammBuilder;

namespace TelegrammEvaluator
{
 
    public enum WindowState 
    {
        eUnknown,
        eOk,
        eWindowIsOpen
    }

    public enum DoorState 
    {
        eUnknown,
        eOk,
        eDoorIsOpen
    }

    public class EvaluatorEventArgsWindow : EventArgs
    {
        public WindowState State { get; set; }
    }

    public class EvaluatorEventArgsDoor : EventArgs
    {
        public DoorState State { get; set; }
    }
    public delegate void Informer( object sender, EventArgs e );
     
    public abstract class TelegrammEvaluator : ITelegrammEvaluator
    {
        protected string _receivedTelegramm;
        public virtual string ReceivedTelegramm { get => _receivedTelegramm; set => _receivedTelegramm = value; }
        public event Informer EInformer;
    }

    public class WindowTelegrammEvaluator : TelegrammEvaluator
    {
        EvaluatorEventArgsWindow WindowArgs = new EvaluatorEventArgsWindow();
        public delegate void Informer( object sender, EvaluatorEventArgsWindow e );

        public new event Informer EInformer;
        public override string ReceivedTelegramm 
        { 
            set 
            {
                _receivedTelegramm  = value;  

            } 
        }

        void Evaluate( string telegramm )
        {
            if( telegramm.Contains( TelegrammTypes.WindowInformation ) )
            {
                if( telegramm.Contains( DeviceStatus.Open ) )
                {
                    WindowArgs.State = WindowState.eWindowIsOpen;
                    EInformer?.Invoke( this, WindowArgs );
                }
            }
                   
        }
    }

    public class DoorTelegrammEvaluator : TelegrammEvaluator
    {
        public delegate void Informer( object sender, EvaluatorEventArgsDoor e );
    }

    public class TelegrammBrocker
    {
        
        public TelegrammBrocker()
        {
            foreach( var item in Evaluators )
            {
                item.EInformer += (sender, e) => 
                {
                    EInformer?.Invoke( sender, e );
                };
            }
        }

        public event Informer EInformer;

        List<TelegrammEvaluator> Evaluators = new List<TelegrammEvaluator>()
        {
            new WindowTelegrammEvaluator(),
            new DoorTelegrammEvaluator()
        };

        public delegate void Informer( object sender, EventArgs e );

        string _receivedTelegramm;
        public string ReceivedTelegramm 
        { 
            set 
            {
                _receivedTelegramm  = value;  
                PassTelegramm( _receivedTelegramm );
            } 
        }

        void PassTelegramm( string telegramm )
        {
           foreach( var item in Evaluators )
           {
                item.ReceivedTelegramm = telegramm;
           }
        }
        
    }
}
