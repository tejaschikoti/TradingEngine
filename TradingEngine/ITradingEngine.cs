using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TradingEngine.Core
{
    interface ITradingEngine
    {
        Task Run(CancellationToken token);
    }
}
