using System;
using System.Windows.Input;

namespace BZ2UIEdit.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _canExecute = canExecute ?? (_ => true);
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
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _canExecute = canExecute ?? (_ => true);
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is T param)
            {
                return _canExecute(param);
            }

            return false;
        }

        public bool CanExecute(T parameter)
            => _canExecute(parameter);

        public void Execute(object parameter)
        {
            if (parameter is T param)
                _execute(param);
            else
                throw new ArgumentException($"Parameter must be typeof {typeof(T).Name}", nameof(parameter));
        }
        public void Execute(T parameter)
            => _execute(parameter);
    }
}
