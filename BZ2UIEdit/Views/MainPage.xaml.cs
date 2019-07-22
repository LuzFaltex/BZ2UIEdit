using BZ2UIEdit.ViewModels;
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
using BZ2UIEdit.Commands;

namespace BZ2UIEdit.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(MainWindow mainWindow, MainPageViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            MainMenu.DataContext = mainWindow;
        }
    }
}
