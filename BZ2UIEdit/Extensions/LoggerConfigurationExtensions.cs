using BZ2UIEdit.Sinks;
using BZ2UIEdit.UserControls;
using Serilog;
using Serilog.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BZ2UIEdit.Extensions
{
    public static class LoggerConfigurationExtensions
    {
        public static LoggerConfiguration GuiLogger(this LoggerSinkConfiguration loggerConfiguration, LogViewer logViewer)
        {
            return loggerConfiguration.Sink(new GuiSink(logViewer));
        }
    }
}
