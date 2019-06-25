using System;
using System.Windows.Input;

namespace BZ2UIEdit.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _canExecute = canExecute ?? returnTrue;
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
            => _canExecute(parameter);

        public void Execute(object parameter)
            => _execute(parameter);

        private static bool returnTrue(object _)
            => true;
    }
}
