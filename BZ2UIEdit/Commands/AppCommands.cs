using BZ2UIEdit.Windows;
using System;
using System.Windows;
using System.Windows.Input;

namespace BZ2UIEdit.Commands
{
    public class AppCommands
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
    }
}
