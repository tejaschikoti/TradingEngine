﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngine.Orderbook
{
    interface IMatchingOrderBook : IRetrievalOrderbook
    {
        MatchResult Match();
    }
}
