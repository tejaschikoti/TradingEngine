using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngine.Logging
{
    public record LogInformation(LogLevel LogLevel, string Module,string Message,DateTime Now, int ThreadId,string ThreadName);

}

namespace System.Runtime.CompilerServices {

    internal static class IsExternalInit { };
}
