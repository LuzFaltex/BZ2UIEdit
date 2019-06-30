using BZ2UIEdit.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;

namespace BZ2UIEdit.ViewModels
{
    public class LogEntry : ViewModelBase
    {
        private DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { SetProperty(ref _dateTime, value); }
        }

        private LogEventLevel _severity;
        public LogEventLevel Severity
        {
            get { return _severity; }
            set { SetProperty(ref _severity, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
    }

    public class CollapsibleLogEntry : LogEntry
    {
        public List<LogEntry> Contents { get; set; } 
            = new List<LogEntry>();
    }
}
