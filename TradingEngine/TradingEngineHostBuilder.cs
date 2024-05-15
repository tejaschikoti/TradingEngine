using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TradingEngine.Core.Configuration;
using TradingEngine.Logging;
using TradingEngine.Logging.LoggingConfiguration;


namespace TradingEngine.Core
{
    public sealed class TradingEngineHostBuilder
    {
        public static IHost BuildTradingEngine()
            => Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddOptions();
                services.Configure<TradingEngineConfiguration>(hostContext.Configuration.GetSection(nameof(TradingEngineConfiguration)));
                services.Configure<LoggerConfiguration>(hostContext.Configuration.GetSection(nameof(TradingEngineConfiguration)));
                services.AddSingleton<ITradingEngine, TradingEngine>();
                services.AddSingleton<ITradingEngine, TradingEngine>();
                services.AddSingleton<ITextLogger,TextLogger>();
                services.AddHostedService<TradingEngine>();

            }).Build();
    }
}
