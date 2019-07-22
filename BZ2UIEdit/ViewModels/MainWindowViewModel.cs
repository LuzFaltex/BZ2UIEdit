using BZ2UIEdit.Commands;
using BZ2UIEdit.Common.Models;
using BZ2UIEdit.Windows;
using System.Windows;
using System.Windows.Input;

namespace BZ2UIEdit.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _applicationStatus;
        public string ApplicationStatus
        {
            get { return _applicationStatus; }
            set { SetProperty(ref _applicationStatus, value); }
        }
    }
}
