using BZ2UIEdit.Commands;
using BZ2UIEdit.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BZ2UIEdit.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand NewProjectCommand { get; }
            = new RelayCommand(_ =>
            {
                new NewProjectDialog().ShowDialog();
            });

        public ICommand CloseCommand { get; }
            = new RelayCommand(
                _ => Application.Current.MainWindow.Close(),
                _ => (Application.Current != null && Application.Current.MainWindow != null));

        public ICommand ShowHelpCommand { get; }
            = new RelayCommand(
                _ => System.Diagnostics.Process.Start("https://github.com/LuzFaltex/BZ2UIEdit/wiki"));

        public ICommand ShowAboutCommand { get; }
            = new RelayCommand(
                _ => new AboutDialog().ShowDialog());



        public string ApplicationStatus
        {
            get { return (string)GetValue(ApplicationStatusProperty); }
            set { SetValue(ApplicationStatusProperty, value); }
        }

        public static readonly DependencyProperty ApplicationStatusProperty =
            DependencyProperty.Register("ApplicationStatus", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata("Ready"));
    }
}
