using BZ2UIEdit.ViewModels;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BZ2UIEdit.UserControls
{
    /// <summary>
    /// Interaction logic for LogViewer.xaml
    /// </summary>
    public partial class LogViewer : UserControl
    {

        public ObservableCollection<LogEntry> LogEntries { get; }
            = new ObservableCollection<LogEntry>();

        public LogViewer()
        {
            DataContext = LogEntries;
            InitializeComponent();            
        }

        public void Log(LogEvent logEvent)
        {
            if (logEvent.Exception is null)
            {
                LogEntry entry = new LogEntry()
                {
                    DateTime = logEvent.Timestamp.UtcDateTime,
                    Severity = logEvent.Level,
                    Message = logEvent.RenderMessage()
                };
                LogEntries.Insert(0, entry);
            }
            else
            {
                CollapsibleLogEntry entry = new CollapsibleLogEntry()
                {
                    DateTime = logEvent.Timestamp.UtcDateTime,
                    Severity = logEvent.Level,
                    Message = string.Join(" - ", logEvent.RenderMessage(), logEvent.Exception.Message)
                };
               
                if (logEvent.Exception is AggregateException aggregate)
                {
                    var flatAggregate = aggregate.Flatten();

                    foreach (Exception ex in flatAggregate.InnerExceptions)
                    {
                        entry.Contents.Add(GetLogEntry(ex, logEvent.Timestamp.UtcDateTime, logEvent.Level));
                    }
                }
                else
                {
                    entry.Contents.Add(GetLogEntry(logEvent.Exception, logEvent.Timestamp.UtcDateTime, logEvent.Level));
                }

                LogEntries.Insert(0, entry);
            }            
        }

        public LogEntry GetLogEntry(Exception exception, DateTime timestamp, LogEventLevel severity)
        {
            /*
            if (exception.InnerException != null)
            {
                CollapsibleLogEntry logEntry = new CollapsibleLogEntry()
                {
                    DateTime = timestamp,
                    Message = exception.ToString(),
                    Severity = severity,
                };

                if (exception.InnerException is AggregateException aggregate)
                {
                    foreach(Exception ex in aggregate.Flatten().InnerExceptions)
                    {
                        logEntry.Contents.Add(GetLogEntry(ex, timestamp, severity));
                    }
                }
                else
                {
                    logEntry.Contents.Add(GetLogEntry(exception.InnerException, timestamp, severity));
                }                
                return logEntry;                
            }
            */

            return new LogEntry()
            {
                Message = exception.ToString(),
                DateTime = timestamp,
                Severity = severity
            };
        }
    }
}
