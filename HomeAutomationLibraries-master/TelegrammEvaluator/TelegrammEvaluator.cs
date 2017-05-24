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
        eAnyWindowIsOpen,
        eAllWindowsAreClosed,
        eInvalid
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

    public delegate void Informer(object sender, EventArgs e);

    public abstract class TelegrammEvaluator : ITelegrammEvaluator
    {
        protected string _receivedTelegramm;
        public virtual string ReceivedTelegramm { get => _receivedTelegramm; set => _receivedTelegramm = value; }
        public event Informer EInformer;
    }

    public class WindowTelegrammEvaluator : TelegrammEvaluator
    {
        EvaluatorEventArgsWindow WindowArgs = new EvaluatorEventArgsWindow();
        public delegate void Informer(object sender, EvaluatorEventArgsWindow e);

        public new event Informer EInformer;
        public override string ReceivedTelegramm
        {
            set
            {
                _receivedTelegramm = value;
                Evaluate(_receivedTelegramm);

            }
        }

        void Evaluate(string telegramm)
        {
            if (telegramm.Contains(TelegrammTypes.WindowInformation))
            {
                if (telegramm.Contains(DeviceStatus.Unknown))
                {
                    WindowArgs.State = WindowState.eUnknown;
                    EInformer?.Invoke(this, WindowArgs);
                    return;
                }

                if (telegramm.Contains(DeviceStatus.Open))
                {
                    WindowArgs.State = WindowState.eAnyWindowIsOpen;
                    EInformer?.Invoke(this, WindowArgs);
                    return;
                }

                WindowArgs.State = WindowState.eAllWindowsAreClosed;
                EInformer?.Invoke(this, WindowArgs);
            }
        }
    }

    public class DoorTelegrammEvaluator : TelegrammEvaluator
    {
        public delegate void Informer(object sender, EvaluatorEventArgsDoor e);
        EvaluatorEventArgsDoor DoorArgs = new EvaluatorEventArgsDoor();
        public new event Informer EInformer;

        public override string ReceivedTelegramm
        {
            set
            {
                _receivedTelegramm = value;
                Evaluate(_receivedTelegramm);
            }
        }

        void Evaluate(string telegramm)
        {
            // Test 
            EInformer?.Invoke(this, DoorArgs);
        }
    }



    public class EvaluatorCollection
    {
        List<TelegrammEvaluator> _Collection = new List<TelegrammEvaluator>()
        {
            new WindowTelegrammEvaluator(),
            new DoorTelegrammEvaluator()
        };

        public List<TelegrammEvaluator> Collection { get => _Collection; set => _Collection = value; }
    }

    public class TelegrammBrocker
    {
        EvaluatorCollection _Evaluators;

        public TelegrammBrocker( EvaluatorCollection Evaluators )
        {
            _Evaluators = Evaluators;
            foreach (var item in Evaluators.Collection)
            {
                item.EInformer += (sender, e) =>
                {
                    EInformer?.Invoke(sender, e);
                };
            }
        }

        public event Informer EInformer;


        public delegate void Informer(object sender, EventArgs e);

        string _receivedTelegramm;
        public string ReceivedTelegramm
        {
            set
            {
                _receivedTelegramm = value;
                PassTelegramm(_receivedTelegramm);
            }
        }

        void PassTelegramm(string telegramm)
        {
            foreach (var item in _Evaluators.Collection )
            {
                item.ReceivedTelegramm = telegramm;
            }
        }

    }
}
