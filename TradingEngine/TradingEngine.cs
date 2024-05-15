using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TradingEngine.Core.Configuration;
using TradingEngine.Logging;

namespace TradingEngine.Core
{
    sealed class TradingEngine : BackgroundService, ITradingEngine
    {
        // private readonly ILogger<TradingEngine> _logger;
        private readonly ITextLogger _logger;
        private readonly TradingEngineConfiguration _tradingEngineConfig;
        public TradingEngine(ITextLogger textLogger,IOptions<TradingEngineConfiguration> config)
        {
            _logger = textLogger ?? throw new ArgumentNullException(nameof(textLogger));
            _tradingEngineConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public Task Run(CancellationToken token) => ExecuteAsync(token);
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information(nameof(TradingEngine), "Starting Trading Engine");
            while (!stoppingToken.IsCancellationRequested)
            {

            }
            _logger.Information(nameof(TradingEngine), "Stopping Trading Engine");

            return Task.CompletedTask;
        }
    }
}
