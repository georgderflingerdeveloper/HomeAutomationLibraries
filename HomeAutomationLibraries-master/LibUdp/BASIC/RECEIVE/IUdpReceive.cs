using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibUdp.BASIC.RECEIVE
{
    public interface IUdpReceive
    {
        event DataReceived EDataReceived;
    }
}
