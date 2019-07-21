using BZ2UIEdit.Common;
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
        private readonly object _lock = new object();
        public ObservableCollection<LogEntry> LogEntries { get; }
            = new ObservableCollection<LogEntry>();
        


        public LogViewer()
        {
            DataContext = LogEntries;
            BindingOperations.EnableCollectionSynchronization(LogEntries, _lock);
            InitializeComponent();            
        }

        private void AddLogEntry(LogEntry entry)
        {
            lock(_lock)
            {
                LogEntries.Insert(0, entry);
            }
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
                AddLogEntry(entry);
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

                AddLogEntry(entry);
            }            
        }

        public LogEntry GetLogEntry(Exception exception, DateTime timestamp, LogEventLevel severity)
        {
            return new LogEntry()
            {
                Message = exception.ToString(),
                DateTime = timestamp,
                Severity = severity
            };
        }
    }
}
