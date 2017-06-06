using System;


namespace TelegrammEvaluator
{
    public class TelegrammBrocker
    {
        EvaluatorCollection _Evaluators;

        public TelegrammBrocker( EvaluatorCollection Evaluators )
        {
            _Evaluators = Evaluators;
            foreach ( var item in Evaluators.Collection )
            {
                item.EInformer += TelegrammBrocker_EInformer;
               //item.EInformer += ( sender, e ) =>
                //{
                //    EInformer?.Invoke( sender, e );
                //};
            }
        }

        private void TelegrammBrocker_EInformer( object sender, EventArgs e )
        {
            EInformer?.Invoke( sender, e );
        }

        public event Informer EInformer;

        public delegate void Informer( object sender, EventArgs e );

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
            foreach ( var item in _Evaluators.Collection )
            {
                item.ReceivedTelegramm = telegramm;
            }
        }

    }
}
