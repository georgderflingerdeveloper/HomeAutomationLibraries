namespace TelegrammBuilder
{
    interface ITelegrammBuilder
    {
       void GotIoChange( int index, bool value );
       event NewTelegramm ENewTelegramm;
    }
}
