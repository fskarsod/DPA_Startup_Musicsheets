using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPA_Musicsheets.Command
{
    public interface ICancelCommand : ICommand
    { }

    public class CancelCommand : BaseCommandWpf, ICancelCommand
    {
        public override void Execute(object parameter)
        {
            var cancelEventArgs = parameter as CancelEventArgs;
            if (cancelEventArgs != null)
                cancelEventArgs.Cancel = true;
        }
    }
}
