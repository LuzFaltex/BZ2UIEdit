using BZ2UIEdit.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
