﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingEngine.Orders;

namespace TradingEngine.Rejects
{
    public sealed class RejectCreator
    {
        public static Rejection GenerateOrderCoreRejection(IOrderCore rejectedOrder, RejectionReason rejectionReason)
        {
            return new Rejection(rejectedOrder, rejectionReason);
        }
    }
}
