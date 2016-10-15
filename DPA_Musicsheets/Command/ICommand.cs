using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Command
{
    public interface ICommand<in TArgument, out TReturn>
    {
        bool CanExecute(TArgument argument);

        TReturn Execute(TArgument argument);

        event EventHandler CanExecuteChanged;
    }
}
