using System;
using TelegrammEvaluator.INTERFACE;
using System.Collections.Generic;
using TelegrammBuilder;

namespace TelegrammEvaluator
{
    public delegate void Informer( object sender, object e );

    public abstract class TelegrammEvaluator : ITelegrammEvaluator
    {
        protected string _receivedTelegramm;
        public virtual string ReceivedTelegramm { get => _receivedTelegramm; set => _receivedTelegramm = value; }
        public virtual event Informer EInformer;
   }
}
