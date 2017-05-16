using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegrammBuilder
{
    interface ITelegrammBuilder
    {
       void GotIoChange( int index, bool value );
       event NewTelegramm ENewTelegramm;
    }
}
