using System;
using System.Collections.Generic;


namespace TelegrammEvaluator
{
    public class TelegrammBrocker
    {
        EvaluatorCollection _Evaluators = new EvaluatorCollection();
        List<TelegrammEvaluator> TestList = new List<TelegrammEvaluator>() 
        {
            new WindowTelegrammEvaluator(),
            new WindowTelegrammEvaluator()
        };

        public TelegrammBrocker( EvaluatorCollection Evaluators )
        {

            foreach ( TelegrammEvaluator Evaluator in _Evaluators.Collection )
            {
                Evaluator.EInformer += TelegrammBrocker_EInformer;
            }
        }

        private void TelegrammBrocker_EInformer( object sender, object e )
        {
            BEInformer?.Invoke( sender, e );
        }

        public event Informer BEInformer;

        public delegate void Informer( object sender, object e );

        string _receivedTelegramm;
        public string ReceivedTelegramm
        {
            set
            {
                _receivedTelegramm = value;
               PassTelegramm( _receivedTelegramm );
            }
        }

        void PassTelegramm( string telegramm )
        {
            foreach ( TelegrammEvaluator Evaluator in _Evaluators.Collection )
            {
                Evaluator.ReceivedTelegramm = telegramm;
            }
        }

    }
}
