using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngine.Orders 
{
    public interface IOrderCore
    {
        public long OrderId { get;  }
        public string Username {get ;}

        public int SecurityId { get ;  }
    }
}
