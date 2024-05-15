﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngine.Orders
{
    public enum RejectionReason
    {
        Unknown,
        OrderNotFound,
        InstrumentNotFound,
        AttemptingToModifyWrongSide,
    }
}
