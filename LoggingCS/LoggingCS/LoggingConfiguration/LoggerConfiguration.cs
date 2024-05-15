﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngine.Logging.LoggingConfiguration
{
    public class LoggerConfiguration
    {
        public LoggerType LoggerType { get; set; }
        public TextLoggerConfiguration TextLoggerConfiguration { get; set; }
    }

    public class TextLoggerConfiguration 
    { 
        public string Directory { get; set; }
        public string Filename { get; set; }
        public string FileExtension { get; set; }
    
    }
}
