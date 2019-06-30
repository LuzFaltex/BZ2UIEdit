using BZ2UIEdit.Commands;
using BZ2UIEdit.Extensions;
using BZ2UIEdit.UserControls;
using BZ2UIEdit.ViewModels;
using BZ2UIEdit.Views;
using Serilog;
using System;
using System.Collections.Generic;
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

namespace BZ2UIEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static readonly string ApplicationBase
            = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LuzFaltex", "BZ2UIEdit");

        public MainWindow()
        {
            
            DataContext = new MainWindowViewModel();
            InitializeComponent();
            MainPage mainPage = new MainPage();
            MainFrame.Navigate(mainPage);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile($@"{ApplicationBase}\Logs\{{Date}}.log", flushToDiskInterval: new TimeSpan(0, 5, 0))
                .WriteTo.GuiLogger((LogViewer)mainPage.FindName("LogControl"))
                .CreateLogger();

            Log.Information("Startup complete");
        }
    }
}
