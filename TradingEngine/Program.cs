using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TradingEngine.Core;
using var engine = TradingEngineHostBuilder.BuildTradingEngine();
TradingEngineServiceProvider.ServiceProvider = engine.Services;

{
    using var scope = TradingEngineServiceProvider.ServiceProvider.CreateScope();
    await engine.RunAsync().ConfigureAwait(false);
}



