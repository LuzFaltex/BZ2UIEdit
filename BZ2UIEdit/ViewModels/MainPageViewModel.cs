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
        private AppCommands _appCommands;
        public AppCommands AppCommands
        {
            get { return _appCommands; }
            set { SetProperty(ref _appCommands, value); }
        }

        public ICommand CollapseSidebarCommand { get; }
            = new RelayCommand(parameter =>
            {
                if (parameter is TabItem tab)
                {
                    tab.IsSelected = true;
                }                
            });


    }
}
