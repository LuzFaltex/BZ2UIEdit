using BZ2UIEdit.UserControls;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BZ2UIEdit.Sinks
{
    public class GuiSink : ILogEventSink
    {
        private readonly LogViewer _logViewer;
        public GuiSink(LogViewer logViewer)
        {
            _logViewer = logViewer;
        }

        [DebuggerStepThrough]
        public void Emit(LogEvent logEvent)
        {
            _logViewer?.Log(logEvent);
        }
    }
}
