using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPA_Musicsheets.Util
{
    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            : base(execute, canExecute)
        { }
    }

    public class RelayCommand<T> : ICommand
        where T : class
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter as T) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter as T);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
