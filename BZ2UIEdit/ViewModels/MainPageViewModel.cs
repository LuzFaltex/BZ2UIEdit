using BZ2UIEdit.Commands;
using BZ2UIEdit.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BZ2UIEdit.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand CollapseSidebarCommand { get; } 
            = new RelayCommand(parameter =>
            {
                if (parameter is TabItem tab)
                {
                    tab.IsSelected = true;
                }                
            });

        public ICommand NewProjectCommand { get; }
            = new RelayCommand(_ =>
            {
                new NewProjectDialog().ShowDialog();
            });

        public ICommand CloseCommand { get; }
            = new RelayCommand(
                _ => Application.Current.MainWindow.Close(),
                _ => (Application.Current != null && Application.Current.MainWindow != null));
    }
}
