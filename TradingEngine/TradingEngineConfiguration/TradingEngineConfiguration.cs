using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngine.Core.Configuration
{
    class TradingEngineConfiguration
    {
        public TradingEngineSettings TradingEngineSettings { get; set; }
    }
    class TradingEngineSettings 
    { 
        public int Port { get; set; }
    }
}
