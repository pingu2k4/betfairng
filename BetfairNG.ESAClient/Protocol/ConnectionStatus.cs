﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetfairNG.ESAClient.Protocol
{
    public enum ConnectionStatus
    {
        STOPPED,
        CONNECTED,
        AUTHENTICATED,
        SUBSCRIBED,
        DISCONNECTED,
    }
}
