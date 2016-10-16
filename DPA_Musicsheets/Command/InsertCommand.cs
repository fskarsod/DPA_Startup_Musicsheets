using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPA_Musicsheets.Command
{
    public interface IInsertCommand : ICommand
    { }

    public class InsertCommand : BaseCommandWpf, IInsertCommand
    {
        private readonly IApplicationContext _applicationContext;

        public InsertCommand(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public override void Execute(object parameter)
        {
            var stringified = parameter as string;
            if(stringified != null)
                _applicationContext.EditorMemento.Content += stringified;
        }
    }
}
