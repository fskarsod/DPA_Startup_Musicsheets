using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPA_Musicsheets.Command
{
    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            : base(execute, canExecute)
        { }
    }

    public class RelayCommand<T> : BaseCommandWpf
        where T : class
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter as T) ?? true;
        }

        public override void Execute(object parameter)
        {
            _execute?.Invoke(parameter as T);
        }
    }
}
