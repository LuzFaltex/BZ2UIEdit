using BZ2UIEdit.ViewModels;
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

        public ObservableCollection<LogEntry> LogEntries { get; set; }
            = new ObservableCollection<LogEntry>();

        public LogViewer()
        {
            DataContext = LogEntries;
            InitializeComponent();            
        }
    }
}
